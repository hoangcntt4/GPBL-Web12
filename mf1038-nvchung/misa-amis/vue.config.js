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
    devtool: "source-map",
  },
};
module.exports = config;
