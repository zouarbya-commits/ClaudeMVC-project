/**
 * ClaudeMVC - JavaScript principal
 * .NET 9 | Bootstrap 5.3 | ES2024
 */

'use strict';

// ============================================================
// Initialisation au chargement du DOM
// ============================================================
document.addEventListener('DOMContentLoaded', () => {
    initTooltips();
    initActiveNavLink();
    initScrollReveal();
    initThemeToggle();
});

// ============================================================
// Tooltips Bootstrap
// ============================================================
function initTooltips() {
    const tooltipElements = document.querySelectorAll('[data-bs-toggle="tooltip"]');
    tooltipElements.forEach(el => new bootstrap.Tooltip(el));
}

// ============================================================
// Active nav link basé sur l'URL courante
// ============================================================
function initActiveNavLink() {
    const currentPath = window.location.pathname;
    document.querySelectorAll('.nav-link').forEach(link => {
        const href = link.getAttribute('href');
        if (href && (currentPath === href || currentPath.startsWith(href + '/'))) {
            link.classList.add('active');
        }
    });
}

// ============================================================
// Scroll reveal avec IntersectionObserver
// ============================================================
function initScrollReveal() {
    const observer = new IntersectionObserver(
        (entries) => {
            entries.forEach(entry => {
                if (entry.isIntersecting) {
                    entry.target.classList.add('animate-fade-up');
                    observer.unobserve(entry.target);
                }
            });
        },
        { threshold: 0.1 }
    );

    document.querySelectorAll('.card').forEach(card => observer.observe(card));
}

// ============================================================
// Toggle thème clair / sombre
// ============================================================
function initThemeToggle() {
    const savedTheme = localStorage.getItem('theme') ?? 'light';
    applyTheme(savedTheme);

    const toggleBtn = document.getElementById('theme-toggle');
    if (toggleBtn) {
        toggleBtn.addEventListener('click', () => {
            const current = document.documentElement.getAttribute('data-bs-theme') ?? 'light';
            const next = current === 'dark' ? 'light' : 'dark';
            applyTheme(next);
            localStorage.setItem('theme', next);
        });
    }
}

function applyTheme(theme) {
    document.documentElement.setAttribute('data-bs-theme', theme);
}

// ============================================================
// Utilitaire : afficher une alerte Bootstrap
// ============================================================
function showAlert(message, type = 'info', container = document.body) {
    const alert = document.createElement('div');
    alert.className = `alert alert-${type} alert-dismissible fade show position-fixed top-0 end-0 m-3`;
    alert.style.zIndex = '9999';
    alert.innerHTML = `
        ${message}
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Fermer"></button>
    `;
    container.appendChild(alert);
    setTimeout(() => alert.remove(), 5000);
}

// Expose les utilitaires globalement
window.ClaudeMVC = { showAlert, applyTheme };
