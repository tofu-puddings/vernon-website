import React from "react";
import { createRoot } from "react-dom/client";
import AppreciationBoard from "./AppreciationBoard";
import "./AppreciationBoard.css";

const rootEl = document.getElementById("appreciation-root");
if (rootEl) {
  createRoot(rootEl).render(
    <React.StrictMode>
      <AppreciationBoard />
    </React.StrictMode>
  );
}