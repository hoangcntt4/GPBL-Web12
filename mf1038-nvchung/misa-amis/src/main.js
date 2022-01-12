import { createApp } from "vue";
import formatter from "./utils/formatter";
import axios from "axios";
import App from "./App.vue";

const app = createApp(App);
app.config.globalProperties.$formatters = formatter;
app.config.globalProperties.$axios = axios;
app.mount("#app");
