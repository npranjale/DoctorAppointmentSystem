// Progressive enhancement: avoid relying on JavaScript for essential form behavior.
document.addEventListener("DOMContentLoaded", () => {
  document.querySelectorAll("[data-confirm]").forEach((element) => {
    element.addEventListener("click", (event) => {
      if (!window.confirm(element.getAttribute("data-confirm"))) event.preventDefault();
    });
  });
});
