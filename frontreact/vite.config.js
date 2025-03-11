import { defineConfig } from 'vite';
import plugin from '@vitejs/plugin-react';

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [plugin()],
    server: {
        https: false,
        host: true,
        port: 55205,
        strictPort: true,
        allowedHosts: true,
        proxy: {
            '/api': 'http://equipo48.duckdns.org:5069'
        }

    }
})