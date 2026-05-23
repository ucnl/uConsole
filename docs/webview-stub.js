// webview-stub.js — заглушка Web Serial API для WebView
(function() {
    if (navigator.serial) {
        console.log('[Stub] Real Web Serial API detected, skipping');
        return;
    }
    
    console.log('[Stub] Creating virtual serial port');
    
    var _port = {
        _readable: null,
        _writable: null,
        
        get readable() {
            if (!this._readable) {
                this._readable = new ReadableStream({
                    start: function(controller) {
                        window._stubController = controller;
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
                        if (window._uartWriteCallback) {
                            window._uartWriteCallback(text);
                        }
                    }
                });
            }
            return this._writable;
        },
        
        open: function(options) {
            this._readable = null;
            this._writable = null;
            return Promise.resolve();
        },
        
        close: function() {
            this._readable = null;
            this._writable = null;
            return Promise.resolve();
        },
        
        getInfo: function() {
            return { usbVendorId: 0x0403, usbProductId: 0x6001 };
        },
        
        forget: function() { return Promise.resolve(); },
        setSignals: function(s) { return Promise.resolve(); },
        getSignals: function() { return Promise.resolve({}); }
    };
    
    navigator.serial = {
        requestPort: function(filters) {
            return Promise.resolve(_port);
        },
        
        getPorts: function() {
            return Promise.resolve([_port]);
        },
        
        addEventListener: function(event, callback) {},
        removeEventListener: function(event, callback) {}
    };
})();