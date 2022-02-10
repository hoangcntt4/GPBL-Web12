import { ref } from "vue";

export default function useMessageDialog() {
  const messageProps = ref({});
  const messageEvents = ref({});
  const close = () => {
    messageProps.value = {};
  };
  const buildEvents = (onYes, onClose, onNo, onCancel) => {
    messageEvents.value = {
      yes: () => {
        close();
        onYes?.();
      },
      no: () => {
        close();
        onNo?.();
      },
      close: () => {
        close();
        onClose?.();
      },
      cancel: () => {
        close();
        onCancel?.();
      },
    };
  };
  const showError = ({ text, onYes, onClose }) => {
    messageProps.value = {
      variant: "info",
      icon: "error",
      text,
      show: true,
    };
    buildEvents(onYes, onClose);
  };
  const showConfirm = ({
    text,
    icon,
    showCancel,
    onYes,
    onClose,
    onNo,
    onCancel,
  }) => {
    messageProps.value = {
      variant: "confirm",
      icon,
      text,
      show: true,
      showCancel,
    };
    buildEvents(onYes, onClose, onNo, onCancel);
  };
  return { messageProps, showError, showConfirm, messageEvents };
}
