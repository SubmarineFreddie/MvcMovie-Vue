import { fileURLToPath, URL } from "node:url"

import vue from "@vitejs/plugin-vue"
import { defineConfig } from "vite"

// https://vitejs.dev/config/
export default defineConfig({
	plugins: [vue()],
	resolve: {
		alias: {
			"@": fileURLToPath(new URL("./src", import.meta.url)),
		},
	},
	build: {
		minify: false,
	},
	server: {
		port: 5143,
		proxy: {
			"/api": {
				target: "http://localhost:5142",
				changeOrigin: false,
				secure: false,
			},
		},
	},
})
