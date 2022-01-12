const path = require("path");
/**
 * @type {import("@vue/cli-service").ProjectOptions}
 */
const config = {
  configureWebpack: {
    resolve: {
      alias: {
        "@": path.resolve(__dirname, "src"),
      },
    },
  },
};
module.exports = config;
