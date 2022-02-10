import { reactive, ref } from "vue";

export default function useForm(rules) {
  let initialErrors = {},
    initialTouched = {};
  const ruleKeys = Object.keys(rules);
  ruleKeys.forEach((key) => {
    initialErrors[key] = null;
    initialTouched[key] = false;
  });
  const values = ref({});
  const errors = reactive(initialErrors);
  const touched = reactive(initialTouched);
  const isFormChanged = ref(false);
  const validateInput = (key) => {
    if (!rules[key]) return;
    for (const rule of rules[key]) {
      const res = rule(values.value[key], values.value);
      if (typeof res == "string") {
        errors[key] = res;
        return;
      }
    }
    errors[key] = null;
  };
  const handleInput = (evt) => {
    const name = evt.target.name;
    isFormChanged.value = true;
    if (name) {
      validateInput(name);
      touched[name] = true;
    }
  };
  const handleBlur = (evt) => {
    const name = evt.target.name;
    validateInput(name);
    touched[name] = true;
  };
  const validate = () => {
    ruleKeys.forEach((key) => {
      touched[key] = true;
      validateInput(key);
    });
    return ruleKeys.every((key) => !errors[key]);
  };
  const reset = () => {
    values.value = {};
    ruleKeys.forEach((key) => {
      touched[key] = false;
      errors[key] = null;
    });
    isFormChanged.value = false;
  };
  const getInputError = (key) =>
    touched[key] && errors[key] ? errors[key] : undefined;
  return {
    values,
    errors,
    touched,
    handleBlur,
    handleInput,
    validate,
    reset,
    isFormChanged,
    getInputError,
  };
}
