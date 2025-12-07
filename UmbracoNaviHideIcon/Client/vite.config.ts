import { defineConfig } from "vite";

export default defineConfig({
    build: {
        lib: {
            entry: "src/umbraco-navi-hide-icon.ts",
            formats: ["es"],
        },
        outDir: "../wwwroot/",
        emptyOutDir: true,
        sourcemap: true,
        rollupOptions: {
            external: [/^@umbraco/],
        },
    },
    base: "/App_Plugins/UmbracoNaviHideIcon/"
});