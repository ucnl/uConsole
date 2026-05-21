// i18n.js — Lightweight localization for uConsole

const I18n = (() => {
    const strings = {
        en: {
            status_disconnected: 'IDLE',
            status_connected: 'CONNECTED',
            connection_title: 'Connection',
            port: 'Port',
            baudrate: 'Baudrate',
            no_ports: 'No ports found',
            refresh: 'Refresh',
            connect: 'Connect',
            disconnect: 'Disconnect',
            history_title: 'History',
            display_mode: 'Display:',
            mode_text: 'Text',
            mode_hex: 'HEX',
            mode_raw: 'Raw',
            autoscroll: 'Autoscroll',
            copy: 'Copy',
            clear: 'Clear',
            save_text: 'Save',
            send_title: 'Send',
            data_to_send: 'Data to send',
            type_here: 'Type here...',
            send: 'Send',
            cr: 'CR',
            lf: 'LF',
            crlf: 'CRLF',
            nmea_checksum: 'NMEA CS',
            record_binary: 'Record',
            ping_pong: 'Ping-Pong',
            generate_random: 'Generate random HEX',
            generate_and_send: 'Generate & Send',
            new_instance: 'New instance',
            org_name: '(C) UC&NL',
            footer_desc: 'Open Source Serial Terminal',
            log_ready: 'uConsole ready.',
            log_connected: 'Connected to',
            log_disconnected: 'Disconnected.',
            log_error: 'Error:',
            log_record_start: 'Recording started...',
            log_record_stop: 'Recording stopped. {bytes} bytes recorded.',
            log_record_saved: 'Binary data saved as',
            log_copied: 'History copied to clipboard.',
            log_cleared: 'History cleared.',
            log_saved: 'History saved as',
            log_no_data: 'Nothing to save.',
        },
        ru: {
            status_disconnected: 'ОЖИДАНИЕ',
            status_connected: 'ПОДКЛЮЧЕНО',
            connection_title: 'Подключение',
            port: 'Порт',
            baudrate: 'Скорость',
            no_ports: 'Нет доступных портов',
            refresh: 'Обновить',
            connect: 'Подключить',
            disconnect: 'Отключить',
            history_title: 'История',
            display_mode: 'Режим:',
            mode_text: 'Текст',
            mode_hex: 'HEX',
            mode_raw: 'Raw',
            autoscroll: 'Автоскролл',
            copy: 'Копир.',
            clear: 'Очистить',
            save_text: 'Сохр.',
            send_title: 'Отправка',
            data_to_send: 'Данные для отправки',
            type_here: 'Введите данные...',
            send: 'Отпр.',
            cr: 'CR',
            lf: 'LF',
            crlf: 'CRLF',
            nmea_checksum: 'NMEA CS',
            record_binary: 'Запись',
            ping_pong: 'Пинг-понг',
            generate_random: 'Сгенерировать HEX',
            generate_and_send: 'Сген. и отправить',
            new_instance: 'Новый экземпляр',
            org_name: '(C) UC&NL',
            footer_desc: 'Терминал последовательного порта с открытым кодом',
            log_ready: 'uConsole готов.',
            log_connected: 'Подключено к',
            log_disconnected: 'Отключено.',
            log_error: 'Ошибка:',
            log_record_start: 'Запись начата...',
            log_record_stop: 'Запись остановлена. Записано {bytes} байт.',
            log_record_saved: 'Бинарные данные сохранены как',
            log_copied: 'История скопирована в буфер.',
            log_cleared: 'История очищена.',
            log_saved: 'История сохранена как',
            log_no_data: 'Нечего сохранять.',
        }
    };

    let currentLang = 'en';

    function setLanguage(lang) {
        if (strings[lang]) {
            currentLang = lang;
            applyTranslations();
        }
    }

    function translate(key) {
        return strings[currentLang][key] || strings['en'][key] || key;
    }

    function applyTranslations() {
        // Elements with data-i18n attribute
        document.querySelectorAll('[data-i18n]').forEach(el => {
            const key = el.getAttribute('data-i18n');
            const text = translate(key);
            if (el.tagName === 'INPUT' && el.type === 'text') {
                // Skip inputs — they have their own value
            } else if (el.tagName === 'INPUT' && el.type === 'button') {
                el.value = text;
            } else {
                el.textContent = text;
            }
        });

        // Placeholders
        document.querySelectorAll('[data-i18n-placeholder]').forEach(el => {
            const key = el.getAttribute('data-i18n-placeholder');
            el.placeholder = translate(key);
        });
    }

    // Init on DOM ready
    document.addEventListener('DOMContentLoaded', () => {
        // Detect browser language
        const browserLang = navigator.language || navigator.userLanguage || 'en';
        if (browserLang.startsWith('ru')) {
            currentLang = 'ru';
        }
        document.getElementById('langSelector').value = currentLang;
        applyTranslations();
    });

    return { setLanguage, translate, applyTranslations, getLanguage: () => currentLang };
})();