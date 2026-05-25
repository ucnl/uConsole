// app.js — uConsole Serial Terminal

class UConsole {
    constructor() {
        this.serial = new SerialCore();
        this.isConnected = false;
        this.isPingPong = false;
        this.isRecording = false;
        this.recordBuffer = [];
        this.maxHistoryEntries = 500;
        this.hexBytesPerLine = 16;

        this._wireSerialEvents();
        this._initAllListeners();
        this._initKeyboardShortcuts();
        this._initDisplayModeSwitch();
        this.addSystemMessage(I18n.translate('log_ready'));
    }

    // ==================== Serial Events ====================

    _wireSerialEvents() {
        this.serial.onData = (data) => {
            this._handleIncomingData(data);
        };

        this.serial.onError = (err) => {
            this.addSystemMessage(`${I18n.translate('log_error')} ${err.message}`, 'error');
        };

        this.serial.onClose = () => {
            this.isConnected = false;
            this._updateUIState(false);
            this.addSystemMessage(I18n.translate('log_disconnected'));
        };
    }

    // ==================== Init Listeners ====================

    _initAllListeners() {
		// Connect / Disconnect
		document.getElementById('btnConnect').addEventListener('click', () => this._toggleConnection());
		document.getElementById('btnRefreshPorts').addEventListener('click', () => this._refreshPorts());

		// Send
		document.getElementById('btnSend').addEventListener('click', () => this._sendFromInput());
		document.getElementById('sendInput').addEventListener('keydown', (e) => {
			if (e.key === 'Enter' && !e.ctrlKey && !e.shiftKey) {
				e.preventDefault();
				this._sendFromInput();
			}
			if (e.key === 'Enter' && e.ctrlKey) {
				e.preventDefault();
				this._sendCRLF();
			}
		});

		// CR, LF, CRLF
		document.getElementById('btnCR').addEventListener('click', () => this._sendControl(0x0D, '<CR>'));
		document.getElementById('btnLF').addEventListener('click', () => this._sendControl(0x0A, '<LF>'));
		document.getElementById('btnCRLF').addEventListener('click', () => this._sendCRLF());

		// NMEA Checksum
		document.getElementById('btnNMEAChecksum').addEventListener('click', () => this._addNMEAChecksum());

		// Record Binary
		document.getElementById('btnRecordBinary').addEventListener('click', () => this._toggleRecording());

		// Ping-Pong
		document.getElementById('pingPongCb').addEventListener('change', (e) => {
			this.isPingPong = e.target.checked;
		});

		// Autoscroll
		document.getElementById('autoscrollCb').addEventListener('change', () => {
			if (document.getElementById('autoscrollCb').checked) {
				this._scrollToBottom();
			}
		});

		// Copy / Clear / Save
		document.getElementById('btnCopy').addEventListener('click', () => this._copyHistory());
		document.getElementById('btnClear').addEventListener('click', () => this._clearHistory());
		document.getElementById('btnSaveText').addEventListener('click', () => this._saveHistoryText());

		// ================= Dropdown Generate =================
		const setupDropdown = (toggleId, menuId, isSend) => {
			const toggle = document.getElementById(toggleId);
			const menu = document.getElementById(menuId);
			const items = menu.querySelectorAll('.dropdown-item');

			// Открыть/закрыть dropdown
			toggle.addEventListener('click', (e) => {
				e.stopPropagation();
				const isOpen = menu.classList.contains('show');
				// Закрыть все dropdown-ы
				document.querySelectorAll('.dropdown-menu').forEach(m => m.classList.remove('show'));
				document.querySelectorAll('.dropdown-toggle').forEach(t => t.classList.remove('open'));
				if (!isOpen) {
					menu.classList.add('show');
					toggle.classList.add('open');
				}
			});

			// Выбор элемента
			items.forEach(item => {
				item.addEventListener('click', (e) => {
					e.stopPropagation();
					const size = parseInt(item.getAttribute('data-size'));
					const hex = this._generateRandomHex(size);

					if (isSend) {
						// Generate & Send
						document.getElementById('sendInput').value = hex;
						this._sendData(hex);
					} else {
						// Generate only
						document.getElementById('sendInput').value = hex;
						document.getElementById('sendInput').focus();
					}

					// Обновить текст кнопки
					toggle.querySelector('.dropdown-label').textContent = `${size} bytes`;

					// Закрыть dropdown
					menu.classList.remove('show');
					toggle.classList.remove('open');
				});
			});
		};

		// Инициализация обоих dropdown-ов
		setupDropdown('btnGenDropdown', 'dropdownMenuGen', false);
		setupDropdown('btnGenSendDropdown', 'dropdownMenuGenSend', true);

		// Закрытие dropdown при клике вне
		document.addEventListener('click', () => {
			document.querySelectorAll('.dropdown-menu').forEach(m => m.classList.remove('show'));
			document.querySelectorAll('.dropdown-toggle').forEach(t => t.classList.remove('open'));
		});

		// Предотвращаем закрытие при клике внутри dropdown
		document.querySelectorAll('.dropdown-menu').forEach(menu => {
			menu.addEventListener('click', (e) => {
				e.stopPropagation();
			});
		});

		// ================= Фикс клавиатуры на мобильных =================
		const sendInput = document.getElementById('sendInput');
		const sendPanel = document.querySelector('.send-panel');
		const historyContainer = document.getElementById('historyContainer');
		const container = document.querySelector('.container');

		// Сохраняем оригинальные стили панели
		const originalStyles = {
			position: sendPanel.style.position || '',
			bottom: sendPanel.style.bottom || '',
			left: sendPanel.style.left || '',
			right: sendPanel.style.right || '',
			zIndex: sendPanel.style.zIndex || '',
			background: sendPanel.style.background || '',
			padding: sendPanel.style.padding || '',
			margin: sendPanel.style.margin || '',
			borderRadius: sendPanel.style.borderRadius || '',
			boxShadow: sendPanel.style.boxShadow || '',
			width: sendPanel.style.width || ''
		};

		let keyboardActive = false;

		function getKeyboardHeight() {
			// Метод 1: через window.innerHeight
			const screenHeight = Math.max(document.documentElement.clientHeight, window.innerHeight || 0);
			const currentHeight = window.innerHeight;
			
			// Метод 2: через visualViewport (если доступен)
			let visualHeight = currentHeight;
			if (window.visualViewport) {
				visualHeight = window.visualViewport.height;
			}
			
			// Берём минимальную из двух высот
			const effectiveHeight = Math.min(currentHeight, visualHeight);
			const keyboardHeight = screenHeight - effectiveHeight;
			
			return keyboardHeight > 100 ? keyboardHeight : 0;
		}

		function showPanelAboveKeyboard() {
			const keyboardHeight = getKeyboardHeight();
			
			if (keyboardHeight > 0 && document.activeElement === sendInput) {
				if (!keyboardActive) {
					keyboardActive = true;
					
					// Фиксируем панель над клавиатурой
					sendPanel.style.position = 'fixed';
					sendPanel.style.bottom = keyboardHeight + 'px';
					sendPanel.style.left = '10px';
					sendPanel.style.right = '10px';
					sendPanel.style.zIndex = '9999';
					sendPanel.style.background = '#f5f5f5';
					sendPanel.style.padding = '12px 16px';
					sendPanel.style.margin = '0';
					sendPanel.style.borderRadius = '8px 8px 0 0';
					sendPanel.style.boxShadow = '0 -4px 16px rgba(0,0,0,0.2)';
					
					// Уменьшаем историю, чтобы не перекрывалась
					historyContainer.style.maxHeight = '30vh';
					historyContainer.style.transition = 'max-height 0.3s ease';
					
					// Добавляем отступ снизу контейнеру, чтобы контент не обрезался
					container.style.paddingBottom = (keyboardHeight + 150) + 'px';
				} else {
					// Обновляем позицию при изменении размера клавиатуры
					sendPanel.style.bottom = keyboardHeight + 'px';
					container.style.paddingBottom = (keyboardHeight + 150) + 'px';
				}
			}
		}

		function resetPanelPosition() {
			if (keyboardActive) {
				keyboardActive = false;
				
				// Возвращаем оригинальные стили
				sendPanel.style.position = originalStyles.position;
				sendPanel.style.bottom = originalStyles.bottom;
				sendPanel.style.left = originalStyles.left;
				sendPanel.style.right = originalStyles.right;
				sendPanel.style.zIndex = originalStyles.zIndex;
				sendPanel.style.background = originalStyles.background;
				sendPanel.style.padding = originalStyles.padding;
				sendPanel.style.margin = originalStyles.margin;
				sendPanel.style.borderRadius = originalStyles.borderRadius;
				sendPanel.style.boxShadow = originalStyles.boxShadow;
				sendPanel.style.width = originalStyles.width;
				
				// Возвращаем высоту истории
				historyContainer.style.maxHeight = '';
				historyContainer.style.transition = 'max-height 0.3s ease';
				
				// Убираем отступ
				container.style.paddingBottom = '';
				
				// Скроллим наверх
				window.scrollTo({ top: 0, behavior: 'smooth' });
			}
		}

		// Обработчики событий
		sendInput.addEventListener('focus', () => {
			// Несколько попыток с разными задержками для надёжности
			setTimeout(showPanelAboveKeyboard, 200);
			setTimeout(showPanelAboveKeyboard, 400);
			setTimeout(showPanelAboveKeyboard, 800);
		});

		sendInput.addEventListener('blur', () => {
			setTimeout(resetPanelPosition, 300);
		});

		// Отслеживаем изменение размера окна
		let lastCheckTime = 0;
		window.addEventListener('resize', () => {
			const now = Date.now();
			if (now - lastCheckTime > 100) { // Дебаунс
				lastCheckTime = now;
				if (document.activeElement === sendInput) {
					showPanelAboveKeyboard();
				}
			}
		});

		// visualViewport для более точного отслеживания
		if (window.visualViewport) {
			window.visualViewport.addEventListener('resize', () => {
				if (document.activeElement === sendInput) {
					showPanelAboveKeyboard();
				}
			});
			
			window.visualViewport.addEventListener('scroll', () => {
				if (document.activeElement === sendInput) {
					showPanelAboveKeyboard();
				}
			});
		}

		// Дополнительно: отлавливаем тап по полю ввода
		sendInput.addEventListener('touchstart', () => {
			// Предварительно готовим панель
			setTimeout(showPanelAboveKeyboard, 300);
		});

		// New instance links
		const newInstanceHandler = (e) => {
			e.preventDefault();
			window.open(window.location.href, '_blank');
		};
		document.getElementById('newInstanceLink').addEventListener('click', newInstanceHandler);
		document.getElementById('footerNewInstance').addEventListener('click', newInstanceHandler);

		// Language selector
		document.getElementById('langSelector').addEventListener('change', (e) => {
			I18n.setLanguage(e.target.value);
			this._updateConnectionStatus();
		});

		// Refresh ports on first load
		this._refreshPorts();
	}



   // =========================================================

    _initDisplayModeSwitch() {
        const radios = document.querySelectorAll('input[name="displayMode"]');
        radios.forEach(radio => {
            radio.addEventListener('change', () => {
                // Will affect how new entries are displayed
                // Existing entries remain as-is
            });
        });
    }

    _getDisplayMode() {
        const checked = document.querySelector('input[name="displayMode"]:checked');
        return checked ? checked.value : 'text';
    }

    // ==================== Keyboard Shortcuts ====================

    _initKeyboardShortcuts() {
        document.addEventListener('keydown', (e) => {
            const inputFocused = document.activeElement === document.getElementById('sendInput');
            
            if (e.ctrlKey) {
                switch (e.key.toLowerCase()) {
                    case 'l':
                        e.preventDefault();
                        this._toggleConnection();
                        break;
                    case 'r':
                        e.preventDefault();
                        this._refreshPorts();
                        break;
                    case 's':
                        e.preventDefault();
                        this._saveHistoryText();
                        break;
                    case 'c':
                        if (!inputFocused) {
                            e.preventDefault();
                            this._copyHistory();
                        }
                        break;
                    case 'x':
                        e.preventDefault();
                        this._clearHistory();
                        break;
                    case 'n':
                        e.preventDefault();
                        this._addNMEAChecksum();
                        break;
                }
            }

            // Any key focuses the input if not already focused
            if (!e.ctrlKey && !e.altKey && !e.metaKey && !inputFocused) {
                if (e.key.length === 1 || e.key === 'Backspace' || e.key === 'Delete') {
                    document.getElementById('sendInput').focus();
                }
            }
        });
    }

    // ==================== Connection ====================

    async _toggleConnection() {
        if (this.isConnected) {
            await this._disconnect();
        } else {
            await this._connect();
        }
    }

    async _connect() {
        const baudrate = parseInt(document.getElementById('baudrateSelect').value);
        const btn = document.getElementById('btnConnect');
        btn.disabled = true;
        btn.textContent = '...';

        try {
            await this.serial.open(baudrate);
            this.isConnected = true;
            this._updateUIState(true);

            let portName = 'Serial Port';
			try {
				if (this.serial.port && typeof this.serial.port.getInfo === 'function') {
					const info = this.serial.port.getInfo();
					portName = `USB (${info.usbVendorId?.toString(16) || '?'}:${info.usbProductId?.toString(16) || '?'})`;
				}
			} catch(e) {}
            this.addSystemMessage(`${I18n.translate('log_connected')} ${portName} @ ${baudrate}`);
        } catch (err) {
            this.isConnected = false;
            this._updateUIState(false);
            if (err.name !== 'AbortError') {
                this.addSystemMessage(`${I18n.translate('log_error')} ${err.message}`, 'error');
            }
        }
    }

    async _disconnect() {
        await this.serial.close();
        this.isConnected = false;
        this._updateUIState(false);
        this.addSystemMessage(I18n.translate('log_disconnected'));
    }

    async _refreshPorts() {
        const ports = await navigator.serial.getPorts();
        const portSelect = document.getElementById('portSelect');
        portSelect.innerHTML = '';

        if (ports.length > 0) {
            ports.forEach(port => {
                let name = 'Serial Port';
				try {
					if (typeof port.getInfo === 'function') {
						const info = port.getInfo();
						name = info.usbProductName || 
							`USB (${info.usbVendorId?.toString(16) || '?'}:${info.usbProductId?.toString(16) || '?'})`;
					}
				} catch(e) {}
				const option = document.createElement('option');
                option.value = name;
                option.textContent = name;
                portSelect.appendChild(option);
            });
        } else {
            const option = document.createElement('option');
            option.value = '';
            option.textContent = I18n.translate('no_ports');
            portSelect.appendChild(option);
        }
    }

    // ==================== Send / Receive ====================

    async _sendControl(byte, label) {
        if (!this.isConnected) return;
        try {
            const bytes = new Uint8Array([byte]);
            await this.serial.send(bytes);
            this._addHistoryEntry(label, 'tx');
        } catch (err) {
            this.addSystemMessage(`${I18n.translate('log_error')} ${err.message}`, 'error');
        }
    }

    async _sendFromInput() {
        const input = document.getElementById('sendInput');
        const data = input.value.trim();
        if (!data) return;
        await this._sendData(data);
        input.value = '';
    }

    async _sendData(data) {
        if (!this.isConnected) return;

        try {
            const isHex = /^[0-9A-Fa-f\s]+$/.test(data) && data.replace(/\s/g, '').length % 2 === 0;
            let bytes;

            if (isHex) {
                const cleaned = data.replace(/\s/g, '');
                bytes = new Uint8Array(cleaned.length / 2);
                for (let i = 0; i < cleaned.length; i += 2) {
                    bytes[i / 2] = parseInt(cleaned.substring(i, i + 2), 16);
                }
            } else {
                bytes = new TextEncoder().encode(data);
            }

            await this.serial.send(bytes);
            this._addHistoryEntry(data, 'tx');
        } catch (err) {
            this.addSystemMessage(`${I18n.translate('log_error')} ${err.message}`, 'error');
        }
    }

    async _sendCRLF() {
        if (!this.isConnected) return;
        try {
            const bytes = new Uint8Array([0x0D, 0x0A]);
            await this.serial.send(bytes);
            this._addHistoryEntry('<CRLF>', 'tx');
        } catch (err) {
            this.addSystemMessage(`${I18n.translate('log_error')} ${err.message}`, 'error');
        }
    }

    _handleIncomingData(data) {
        const mode = this._getDisplayMode();
        let displayStr;

        switch (mode) {
            case 'hex':
                displayStr = this._formatHex(data, this.hexBytesPerLine);
                break;
            case 'raw':
                displayStr = new TextDecoder().decode(data);
                break;
            case 'text':
            default:
                displayStr = this._safeDecode(data);
                break;
        }

        this._addHistoryEntry(displayStr, 'rx');

        if (this.isRecording) {
            this.recordBuffer.push(new Uint8Array(data));
        }

        if (this.isPingPong && this.isConnected) {
            this.serial.send(data).catch(() => {});
        }
    }

    // ==================== Display Formatting ====================

    _formatHex(data, bytesPerLine) {
        const lines = [];
        for (let i = 0; i < data.length; i += bytesPerLine) {
            const chunk = data.slice(i, i + bytesPerLine);
            const hexPart = Array.from(chunk)
                .map(b => b.toString(16).toUpperCase().padStart(2, '0'))
                .join(' ');
            lines.push(hexPart);
        }
        return lines.join('\n');
    }

    _safeDecode(data) {
        let result = '';
        for (let i = 0; i < data.length; i++) {
            const b = data[i];
            if (b === 0x0D && i + 1 < data.length && data[i + 1] === 0x0A) {
                result += '<CRLF>';
                i++;
            } else if (b === 0x0D) {
                result += '<CR>';
            } else if (b === 0x0A) {
                result += '<LF>';
            } else if (b >= 0x20 && b !== 0x7F) {
                result += String.fromCharCode(b);
            } else {
                result += `0x${b.toString(16).toUpperCase().padStart(2, '0')}`;
            }
        }
        return result;
    }

    // ==================== History ====================

    _addHistoryEntry(text, type) {
        const container = document.getElementById('historyContainer');
        const entry = document.createElement('div');
        entry.className = `log-entry ${type}`;

        const now = new Date();
        const timestamp = `${String(now.getHours()).padStart(2, '0')}:${String(now.getMinutes()).padStart(2, '0')}:${String(now.getSeconds()).padStart(2, '0')}.${String(now.getMilliseconds()).padStart(3, '0')}`;
        entry.textContent = `${timestamp} ${text}`;

        container.appendChild(entry);

        while (container.children.length > this.maxHistoryEntries) {
            container.removeChild(container.firstChild);
        }

        if (document.getElementById('autoscrollCb').checked) {
            this._scrollToBottom();
        }
    }

    addSystemMessage(msg, type = 'system') {
        const container = document.getElementById('historyContainer');
        const entry = document.createElement('div');
        entry.className = `log-entry ${type}`;
        entry.textContent = `--- ${msg}`;
        container.appendChild(entry);

        if (document.getElementById('autoscrollCb').checked) {
            this._scrollToBottom();
        }
    }

    _scrollToBottom() {
        const container = document.getElementById('historyContainer');
        requestAnimationFrame(() => {
            container.scrollTop = container.scrollHeight;
        });
    }

    _copyHistory() {
        const container = document.getElementById('historyContainer');
        const text = Array.from(container.children)
            .map(el => el.textContent)
            .join('\n');

        if (text) {
            navigator.clipboard.writeText(text).then(() => {
                this.addSystemMessage(I18n.translate('log_copied'));
            }).catch(() => {
                this.addSystemMessage('Failed to copy', 'error');
            });
        }
    }

    _clearHistory() {
        const container = document.getElementById('historyContainer');
        container.innerHTML = '';
        this.addSystemMessage(I18n.translate('log_cleared'));
    }

    _saveHistoryText() {
        const container = document.getElementById('historyContainer');
        const text = Array.from(container.children)
            .map(el => el.textContent)
            .join('\n');

        if (!text.trim()) {
            this.addSystemMessage(I18n.translate('log_no_data'), 'error');
            return;
        }

        const now = new Date();
        const filename = `uConsole_${now.getFullYear()}${String(now.getMonth()+1).padStart(2,'0')}${String(now.getDate()).padStart(2,'0')}_${String(now.getHours()).padStart(2,'0')}${String(now.getMinutes()).padStart(2,'0')}${String(now.getSeconds()).padStart(2,'0')}.txt`;

        const blob = new Blob([text], { type: 'text/plain' });
        const url = URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url;
        a.download = filename;
        a.click();
        URL.revokeObjectURL(url);

        this.addSystemMessage(`${I18n.translate('log_saved')} ${filename}`);
    }

    // ==================== Binary Recording ====================

    async _toggleRecording() {
        if (this.isRecording) {
            this.isRecording = false;
            const btn = document.getElementById('btnRecordBinary');
            btn.classList.remove('btn-recording');
            btn.textContent = '⏺ Record';

            const totalBytes = this.recordBuffer.reduce((sum, arr) => sum + arr.length, 0);
            const message = I18n.translate('log_record_stop').replace('{bytes}', totalBytes);
            this.addSystemMessage(message);

            if (totalBytes > 0) {
                const combined = new Uint8Array(totalBytes);
                let offset = 0;
                for (const chunk of this.recordBuffer) {
                    combined.set(chunk, offset);
                    offset += chunk.length;
                }

                const now = new Date();
                const filename = `data_${now.getFullYear()}${String(now.getMonth()+1).padStart(2,'0')}${String(now.getDate()).padStart(2,'0')}_${String(now.getHours()).padStart(2,'0')}${String(now.getMinutes()).padStart(2,'0')}${String(now.getSeconds()).padStart(2,'0')}.bin`;

                const blob = new Blob([combined], { type: 'application/octet-stream' });
                const url = URL.createObjectURL(blob);
                const a = document.createElement('a');
                a.href = url;
                a.download = filename;
                a.click();
                URL.revokeObjectURL(url);

                this.addSystemMessage(`${I18n.translate('log_record_saved')} ${filename}`);
                this.recordBuffer = [];
            }
        } else {
            this.isRecording = true;
            this.recordBuffer = [];
            const btn = document.getElementById('btnRecordBinary');
            btn.classList.add('btn-recording');
            btn.textContent = '⏹ Stop';
            this.addSystemMessage(I18n.translate('log_record_start'));
        }
    }

    // ==================== NMEA Checksum ====================

    _addNMEAChecksum() {
        const input = document.getElementById('sendInput');
        const text = input.value.trim();

        if (text.startsWith('$') && text.endsWith('*')) {
            const content = text.substring(1, text.length - 1);
            let checksum = 0;
            for (let i = 0; i < content.length; i++) {
                checksum ^= content.charCodeAt(i);
            }
            input.value = text + checksum.toString(16).toUpperCase().padStart(2, '0');
        } else {
            this.addSystemMessage('Format: $...* (NMEA sentence without checksum)', 'error');
        }
    }

    // ==================== Random HEX Generation ====================

    _generateRandomHex(size) {
        const bytes = new Uint8Array(size);
        crypto.getRandomValues(bytes);
        return Array.from(bytes)
            .map(b => b.toString(16).toUpperCase().padStart(2, '0'))
            .join('');
    }

    // ==================== UI State ====================

    _updateUIState(connected) {
        const btnConnect = document.getElementById('btnConnect');
        const btnSend = document.getElementById('btnSend');
        const btnCR = document.getElementById('btnCR');
        const btnLF = document.getElementById('btnLF');
        const btnCRLF = document.getElementById('btnCRLF');
        const btnNMEA = document.getElementById('btnNMEAChecksum');
        const btnRecord = document.getElementById('btnRecordBinary');
        const generateSendBtns = document.querySelectorAll('.generate-send-btn');

        if (connected) {
            btnConnect.textContent = I18n.translate('disconnect');
            btnConnect.classList.remove('btn-primary');
            btnConnect.classList.add('btn-secondary');
            btnSend.disabled = false;
            btnCR.disabled = false;
            btnLF.disabled = false;
            btnCRLF.disabled = false;
            btnNMEA.disabled = false;
            btnRecord.disabled = false;
            generateSendBtns.forEach(b => b.disabled = false);
        } else {
            btnConnect.textContent = I18n.translate('connect');
            btnConnect.classList.remove('btn-secondary');
            btnConnect.classList.add('btn-primary');
            btnSend.disabled = true;
            btnCR.disabled = true;
            btnLF.disabled = true;
            btnCRLF.disabled = true;
            btnNMEA.disabled = false;
            btnRecord.disabled = true;
            generateSendBtns.forEach(b => b.disabled = true);
        }

        btnConnect.disabled = false;
        this._updateConnectionStatus();
    }

    _updateConnectionStatus() {
        const badge = document.getElementById('connectionStatus');
        if (this.isConnected) {
            badge.className = 'status-badge connected';
            badge.textContent = I18n.translate('status_connected');
        } else {
            badge.className = 'status-badge disconnected';
            badge.textContent = I18n.translate('status_disconnected');
        }
    }
}

// ==================== Startup ====================

document.addEventListener('DOMContentLoaded', () => {
    window.uConsole = new UConsole();
});