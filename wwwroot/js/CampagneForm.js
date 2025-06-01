// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

document.addEventListener('DOMContentLoaded', function () {
    var form = document.getElementById('campagneForm');
    if (!form) return;
    form.addEventListener('submit', function (e) {
        var textarea = document.querySelector('[name="Recipients"]');
        if (!textarea) return;
        var emails = textarea.value.split(/\r?\n/).map(x => x.trim()).filter(x => x.length > 0);
        var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        let allValid = true;
        for (const email of emails) {
            if (!emailRegex.test(email)) {
                allValid = false;
                break;
            }
        }
        if (!allValid) {
            e.preventDefault();
            alert('Vul alleen geldige e-mailadressen in (één per regel).');
        }
    });
});
