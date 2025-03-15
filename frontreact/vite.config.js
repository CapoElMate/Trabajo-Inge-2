import { defineConfig } from 'vite';
import plugin from '@vitejs/plugin-react';


const puerto = process.env.VITE_APP_PUERTO;
const protocolo = process.env.VITE_APP_PROTOCOLO;


// https://vitejs.dev/config/
export default defineConfig({
    plugins: [plugin()],
    server: {
        port: Number(puerto),
        strictPort: true,
        host: true,
        origin: "${protocolo}://0.0.0.0:${puerto}",
        allowedHosts: true,
    }
})