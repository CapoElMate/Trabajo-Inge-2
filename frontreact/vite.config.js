import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import fs from 'fs'

// https://vite.dev/config/
/*export default defineConfig({
  plugins: [react()],
})
*/

export default {
  plugins: [react()],
  server: {
    proxy: {
      "/api": {
        target: "http://localhost:5000/",
        changeOrigin: true,
        rewrite: (path) => path.replace(/^\/api/, ""),
      },
    },
    https: {
      key: fs.readFileSync("./certs/localhost-key.pem"),
      cert: fs.readFileSync("./certs/localhost.pem"),
    },
  },
};
