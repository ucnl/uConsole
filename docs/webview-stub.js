// webview-stub.js — заглушка Web Serial API для WebView
(function() {
    if (navigator.serial) {
        console.log('[Stub] Real Web Serial API detected, skipping');
        return;
    }
    
    console.log('[Stub] Creating virtual serial port');
    
    function createPort() {
        return {
            _readable: null,
            _writable: null,
            
            get readable() {
                if (!this._readable) {
                    this._readable = new ReadableStream({
                        start: function(controller) {
                            window._stubController = controller;
                            console.log('[Stub] ReadableStream started');
                        }
                    });
                }
                return this._readable;
            },
            
            get writable() {
                if (!this._writable) {
                    this._writable = new WritableStream({
                        write: function(chunk) {
                            var text = typeof chunk === 'string' ? chunk : new TextDecoder().decode(chunk);
                            console.log('[Stub] Write:', text);
                            if (window._uartWriteCallback) {
                                window._uartWriteCallback(text);
                            }
                        }
                    });
                }
                return this._writable;
            },
            
            open: function(options) {
                console.log('[Stub] Port opened with', options);
                // Сбрасываем потоки при открытии
                this._readable = null;
                this._writable = null;
                return Promise.resolve();
            },
            
            close: function() {
                console.log('[Stub] Port closed');
                this._readable = null;
                this._writable = null;
                return Promise.resolve();
            },
            
            getInfo: function() {
                console.log('[Stub] getInfo called');
                return { 
                    usbVendorId: 0x0403, 
                    usbProductId: 0x6001 
                };
            },
            
            forget: function() {
                return Promise.resolve();
            },
            
            setSignals: function(signals) {
                return Promise.resolve();
            },
            
            getSignals: function() {
                return Promise.resolve({
                    dataTerminalReady: true,
                    requestToSend: true,
                    dataCarrierDetect: true,
                    dataSetReady: true,
                    ringIndicator: false,
                    clearToSend: false
                });
            }
        };
    }
    
    var _port = createPort();
    
    navigator.serial = {
        requestPort: function(filters) {
            console.log('[Stub] requestPort called', filters);
            _port = createPort();  // Новый порт при каждом запросе
            return Promise.resolve(_port);
        },
        
        getPorts: function() {
            console.log('[Stub] getPorts called, returning 1 port');
            return Promise.resolve([_port]);
        },
        
        addEventListener: function(event, callback) {
            console.log('[Stub] addEventListener:', event);
        },
        
        removeEventListener: function(event, callback) {
            console.log('[Stub] removeEventListener:', event);
        }
    };
    
    console.log('[Stub] Virtual serial port ready (pre-created)');
})();