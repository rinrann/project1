/* ============================================================
   ANKURAN — Chart.js helpers (themed)
   ============================================================ */
(function () {
  "use strict";
  const reg = [];

  function cssVar(name) { return getComputedStyle(document.documentElement).getPropertyValue(name).trim(); }
  function alpha(hex, a) {
    hex = hex.replace("#","");
    if (hex.length === 3) hex = hex.split("").map(c=>c+c).join("");
    const r = parseInt(hex.slice(0,2),16), g = parseInt(hex.slice(2,4),16), b = parseInt(hex.slice(4,6),16);
    return `rgba(${r},${g},${b},${a})`;
  }
  window.chartAlpha = alpha;

  function base() {
    return {
      accent: cssVar("--accent") || "#9d5c63",
      grid: "rgba(44,37,34,.07)",
      text: cssVar("--text-3") || "#8a7d77",
      font: "Hanken Grotesk",
    };
  }
  window.chartTheme = base;

  // Defaults
  function applyDefaults() {
    if (!window.Chart) return;
    const t = base();
    Chart.defaults.font.family = t.font;
    Chart.defaults.font.size = 12;
    Chart.defaults.color = t.text;
    Chart.defaults.plugins.legend.display = false;
    Chart.defaults.plugins.tooltip.backgroundColor = "#2c2522";
    Chart.defaults.plugins.tooltip.padding = 10;
    Chart.defaults.plugins.tooltip.cornerRadius = 8;
    Chart.defaults.plugins.tooltip.titleFont = { weight: "600", family: t.font };
    Chart.defaults.plugins.tooltip.boxPadding = 4;
    Chart.defaults.maintainAspectRatio = false;
    Chart.defaults.animation = false;
  }

  // create + track for teardown on re-render
  window.makeChart = function (canvas, config) {
    if (!window.Chart) return null;
    applyDefaults();
    const c = new Chart(canvas, config);
    reg.push(c);
    return c;
  };
  window.destroyCharts = function () { while (reg.length) { try { reg.pop().destroy(); } catch (e) {} } };

  // axis presets
  window.axisX = function () { const t = base(); return { grid: { display: false }, border: { display: false }, ticks: { color: t.text } }; };
  window.axisY = function (opts) { opts = opts||{}; const t = base(); return { grid: { color: t.grid }, border: { display: false }, ticks: { color: t.text, callback: opts.fmt, maxTicksLimit: 6 }, beginAtZero: true }; };
})();
