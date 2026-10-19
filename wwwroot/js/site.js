// ===== BootNews JS =====
document.addEventListener("DOMContentLoaded", function () {

    // --- Hero carousel ---
    var carousel = document.getElementById("heroCarousel");
    if (carousel) {
        var slides = carousel.querySelectorAll(".hero-slide");
        var dots = carousel.querySelectorAll(".dot");
        var current = 0;
        var timer = null;

        function show(index) {
            slides[current].classList.remove("active");
            dots[current].classList.remove("active");
            current = (index + slides.length) % slides.length;
            slides[current].classList.add("active");
            dots[current].classList.add("active");
        }

        function restart() {
            clearInterval(timer);
            timer = setInterval(function () { show(current + 1); }, 5000);
        }

        carousel.querySelector(".next").addEventListener("click", function () { show(current + 1); restart(); });
        carousel.querySelector(".prev").addEventListener("click", function () { show(current - 1); restart(); });
        dots.forEach(function (dot) {
            dot.addEventListener("click", function () {
                show(parseInt(dot.getAttribute("data-slide"), 10));
                restart();
            });
        });
        restart();
    }

    // --- Breaking news ticker ---
    var ticker = document.getElementById("breakingTicker");
    if (ticker) {
        var items = ticker.querySelectorAll(".breaking-item");
        var bi = 0;
        function showItem(i) {
            items[bi].classList.remove("active");
            bi = (i + items.length) % items.length;
            items[bi].classList.add("active");
        }
        items[0].classList.add("active");
        setInterval(function () { showItem(bi + 1); }, 4000);
        var bp = document.getElementById("breakPrev");
        var bn = document.getElementById("breakNext");
        if (bp) bp.addEventListener("click", function () { showItem(bi - 1); });
        if (bn) bn.addEventListener("click", function () { showItem(bi + 1); });
    }

    // --- Mobile nav toggle ---
    var toggle = document.getElementById("navToggle");
    var nav = document.getElementById("mainNav");
    if (toggle && nav) {
        toggle.addEventListener("click", function () {
            nav.classList.toggle("open");
        });
    }
});
