// serial-core.js — Minimal Web Serial API wrapper for uConsole
// Provides: open(baudRate), close(), send(data), onData, onError

class SerialCore {
    constructor() {
        this.port = null;
        this.reader = null;
        this.isOpen = false;

        // Callbacks
        this.onData = null;     // (data: Uint8Array) => void
        this.onError = null;    // (error: Error) => void
        this.onClose = null;    // () => void
    }

    /**
     * Request port from user and open with given baud rate
     * @param {number} baudRate
     * @returns {Promise<boolean>}
     */
    async open(baudRate = 9600) {
        try {
            // Must be triggered by user gesture
            this.port = await navigator.serial.requestPort();

            await this.port.open({
                baudRate: baudRate,
                dataBits: 8,
                stopBits: 1,
                parity: 'none',
                flowControl: 'none'
            });

            this.isOpen = true;

            // Start reading
            this.reader = this.port.readable.getReader();
            this._readLoop();

            return true;
        } catch (err) {
            this.isOpen = false;
            if (this.onError) this.onError(err);
            throw err;
        }
    }

    /**
     * Send raw bytes to port
     * @param {Uint8Array} data
     */
    async send(data) {
        if (!this.isOpen || !this.port) throw new Error('Port not open');
        const writer = this.port.writable.getWriter();
        await writer.write(data);
        writer.releaseLock();
    }

    /**
     * Internal read loop — pushes every received chunk to onData immediately
     */
    async _readLoop() {
        try {
            while (true) {
                const { value, done } = await this.reader.read();
                if (done) break;

                if (this.onData && value && value.length > 0) {
                    this.onData(value);
                }
            }
        } catch (err) {
            if (err.name === 'NetworkError' || err.name === 'AbortError') {
                // Port disconnected — normal
            } else if (this.onError) {
                this.onError(err);
            }
        }

        this.isOpen = false;
        if (this.onClose) this.onClose();
    }

    /**
     * Close the port and release all resources
     */
    async close() {
        if (this.reader) {
            try { this.reader.cancel(); } catch (e) { /* ignore */ }
            try { this.reader.releaseLock(); } catch (e) { /* ignore */ }
            this.reader = null;
        }

        if (this.port) {
            try { await this.port.close(); } catch (e) { /* ignore */ }
            this.port = null;
        }

        this.isOpen = false;
    }
}