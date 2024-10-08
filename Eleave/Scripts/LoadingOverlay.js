﻿!function (e) { var t = {}; function n(i) { if (t[i]) return t[i].exports; var o = t[i] = { i: i, l: !1, exports: {} }; return e[i].call(o.exports, o, o.exports, n), o.l = !0, o.exports } n.m = e, n.c = t, n.d = function (e, t, i) { n.o(e, t) || Object.defineProperty(e, t, { enumerable: !0, get: i }) }, n.r = function (e) { "undefined" != typeof Symbol && Symbol.toStringTag && Object.defineProperty(e, Symbol.toStringTag, { value: "Module" }), Object.defineProperty(e, "__esModule", { value: !0 }) }, n.t = function (e, t) { if (1 & t && (e = n(e)), 8 & t) return e; if (4 & t && "object" == typeof e && e && e.__esModule) return e; var i = Object.create(null); if (n.r(i), Object.defineProperty(i, "default", { enumerable: !0, value: e }), 2 & t && "string" != typeof e) for (var o in e) n.d(i, o, function (t) { return e[t] }.bind(null, o)); return i }, n.n = function (e) { var t = e && e.__esModule ? function () { return e.default } : function () { return e }; return n.d(t, "a", t), t }, n.o = function (e, t) { return Object.prototype.hasOwnProperty.call(e, t) }, n.p = "", n(n.s = 0) }([function (e, t, n) { e.exports = n(1) }, function (e, t) { function n(e, t) { for (var n = 0; n < t.length; n++) { var i = t[n]; i.enumerable = i.enumerable || !1, i.configurable = !0, "value" in i && (i.writable = !0), Object.defineProperty(e, i.key, i) } } var i = function () { function e() { !function (e, t) { if (!(e instanceof t)) throw new TypeError("Cannot call a class as a function") }(this, e), this.options = { overlayBackgroundColor: "#666666", overlayOpacity: .6, spinnerIcon: "ball-circus", spinnerColor: "#000", spinnerSize: "3x", overlayIDName: "overlay", spinnerIDName: "spinner", offsetY: 0, offsetX: 0, lockScroll: !1, containerID: null }, this.stylesheetBaseURL = "https://cdn.jsdelivr.net/npm/load-awesome@1.1.0/css/", this.spinner = null, this.spinnerStylesheetURL = null, this.numberOfEmptyDivForSpinner = { "ball-8bits": 16, "ball-atom": 4, "ball-beat": 3, "ball-circus": 5, "ball-climbing-dot": 1, "ball-clip-rotate": 1, "ball-clip-rotate-multiple": 2, "ball-clip-rotate-pulse": 2, "ball-elastic-dots": 5, "ball-fall": 3, "ball-fussion": 4, "ball-grid-beat": 9, "ball-grid-pulse": 9, "ball-newton-cradle": 4, "ball-pulse": 3, "ball-pulse-rise": 5, "ball-pulse-sync": 3, "ball-rotate": 1, "ball-running-dots": 5, "ball-scale": 1, "ball-scale-multiple": 3, "ball-scale-pulse": 2, "ball-scale-ripple": 1, "ball-scale-ripple-multiple": 3, "ball-spin": 8, "ball-spin-clockwise": 8, "ball-spin-clockwise-fade": 8, "ball-spin-clockwise-fade-rotating": 8, "ball-spin-fade": 8, "ball-spin-fade-rotating": 8, "ball-spin-rotate": 2, "ball-square-clockwise-spin": 8, "ball-square-spin": 8, "ball-triangle-path": 3, "ball-zig-zag": 2, "ball-zig-zag-deflect": 2, cog: 1, "cube-transition": 2, fire: 3, "line-scale": 5, "line-scale-party": 5, "line-scale-pulse-out": 5, "line-scale-pulse-out-rapid": 5, "line-spin-clockwise-fade": 8, "line-spin-clockwise-fade-rotating": 8, "line-spin-fade": 8, "line-spin-fade-rotating": 8, pacman: 6, "square-jelly-box": 2, "square-loader": 1, "square-spin": 1, timer: 1, "triangle-skew-spin": 1 }, this.originalBodyPosition = "", this.originalBodyTop = "", this.originalBodywidth = "" } var t, i, o; return t = e, (i = [{ key: "show", value: function (e) { this.setOptions(e), this.addSpinnerStylesheet(), this.generateSpinnerElement(), this.options.lockScroll && (document.body.style.overflow = "hidden", document.documentElement.style.overflow = "hidden"), this.generateAndAddOverlayElement() } }, { key: "hide", value: function () { this.options.lockScroll && (document.body.style.overflow = "", document.documentElement.style.overflow = ""); var e = document.getElementById("loading-overlay-stylesheet"); e && (e.disabled = !0, e.parentNode.removeChild(e), document.getElementById(this.options.overlayIDName).remove(), document.getElementById(this.options.spinnerIDName).remove()) } }, { key: "setOptions", value: function (e) { if (void 0 !== e) for (var t in e) this.options[t] = e[t] } }, { key: "generateAndAddOverlayElement", value: function () { var e = "50%"; 0 !== this.options.offsetX && (e = "calc(50% + " + this.options.offsetX + ")"); var t = "50%"; if (0 !== this.options.offsetY && (t = "calc(50% + " + this.options.offsetY + ")"), this.options.containerID && document.body.contains(document.getElementById(this.options.containerID))) { var n = '<div id="'.concat(this.options.overlayIDName, '" style="display: block !important; position: absolute; top: 0; left: 0; overflow: auto; opacity: ').concat(this.options.overlayOpacity, "; background: ").concat(this.options.overlayBackgroundColor, '; z-index: 50; width: 100%; height: 100%;"></div><div id="').concat(this.options.spinnerIDName, '" style="display: block !important; position: absolute; top: ').concat(t, "; left: ").concat(e, '; -webkit-transform: translate(-50%); -ms-transform: translate(-50%); transform: translate(-50%); z-index: 9999;">').concat(this.spinner, "</div>"), i = document.getElementById(this.options.containerID); return i.style.position = "relative", void i.insertAdjacentHTML("beforeend", n) } var o = '<div id="'.concat(this.options.overlayIDName, '" style="display: block !important; position: fixed; top: 0; left: 0; overflow: auto; opacity: ').concat(this.options.overlayOpacity, "; background: ").concat(this.options.overlayBackgroundColor, '; z-index: 50; width: 100%; height: 100%;"></div><div id="').concat(this.options.spinnerIDName, '" style="display: block !important; position: fixed; top: ').concat(t, "; left: ").concat(e, '; -webkit-transform: translate(-50%); -ms-transform: translate(-50%); transform: translate(-50%); z-index: 9999;">').concat(this.spinner, "</div>"); document.body.insertAdjacentHTML("beforeend", o) } }, { key: "generateSpinnerElement", value: function () { var e = this, t = Object.keys(this.numberOfEmptyDivForSpinner).find((function (t) { return t === e.options.spinnerIcon })), n = this.generateEmptyDivElement(this.numberOfEmptyDivForSpinner[t]); this.spinner = '<div style="color: '.concat(this.options.spinnerColor, '" class="la-').concat(this.options.spinnerIcon, " la-").concat(this.options.spinnerSize, '">').concat(n, "</div>") } }, { key: "addSpinnerStylesheet", value: function () { this.setSpinnerStylesheetURL(); var e = document.createElement("link"); e.setAttribute("id", "loading-overlay-stylesheet"), e.setAttribute("rel", "stylesheet"), e.setAttribute("type", "text/css"), e.setAttribute("href", this.spinnerStylesheetURL), document.getElementsByTagName("head")[0].appendChild(e) } }, { key: "setSpinnerStylesheetURL", value: function () { this.spinnerStylesheetURL = this.stylesheetBaseURL + this.options.spinnerIcon + ".min.css" } }, { key: "generateEmptyDivElement", value: function (e) { for (var t = "", n = 1; n <= e; n++)t += "<div></div>"; return t } }]) && n(t.prototype, i), o && n(t, o), e }(); window.JsLoadingOverlay = new i, e.exports = JsLoadingOverlay }]);
//# sourceMappingURL=js-loading-overlay.min.js.map