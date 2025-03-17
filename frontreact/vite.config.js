import { defineConfig } from 'vite';
import plugin from '@vitejs/plugin-react';
import fs from 'fs';

const puerto = process.env.VITE_APP_PUERTO;
const protocolo = process.env.VITE_APP_PROTOCOLO;
const usoHTTPS = process.env.VITE_APP_USAR_HTTPS;


// https://vitejs.dev/config/
export default defineConfig({
    plugins: [plugin()],
    server: {
        port: Number(puerto),
        strictPort: true,
        host: true,
        origin: "${protocolo}://0.0.0.0:${puerto}",
        allowedHosts: true,
        https: usoHTTPS === 'true' ? {
            key: fs.readFileSync('/app/certificados/cloudflare-key.pem'),
            cert: fs.readFileSync('/app/certificados/cloudflare-cert.pem'),
        } : false
    }
})