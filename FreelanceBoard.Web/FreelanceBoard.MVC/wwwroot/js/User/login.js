
$(document).ready(function () {
    initializePage();
    setupEventListeners();
    setupValidation();
});

function initializePage() {
    // Check if there's a success message from registration
    const successMsg = localStorage.getItem("registrationSuccess");
    if (successMsg) {
        showMessage(successMsg, "success");
        localStorage.removeItem("registrationSuccess");
    }

    // Check if user was redirected from protected page
    const requireLogin = localStorage.getItem("requireLogin");
    if (requireLogin) {
        showMessage("Please login to access that page", "warning");
        localStorage.removeItem("requireLogin");
    }
}

function setupEventListeners() {
    // Password toggle button
    $(document).on('click', '.password-toggle-btn', function () {
        const field = $(this).data('field');
        togglePasswordVisibility(field);
    });


    // Enter key submission
    $('#Email, #Password').on('keypress', function (e) {
        if (e.which === 13) {
            $('#loginForm').submit();
        }
    });
}

function setupValidation() {
    // jQuery Validation setup
    $('#loginForm').validate({
        rules: {
            Email: {
                required: true,
                email: true
            },
            Password: {
                required: true,
                minlength: 6
            }
        },
        messages: {
            Email: {
                required: "Please enter your email address",
                email: "Please enter a valid email address"
            },
            Password: {
                required: "Please enter your password",
                minlength: "Password must be at least 6 characters"
            }
        },
        errorElement: 'div',
        errorClass: 'invalid-feedback',
        highlight: function (element) {
            $(element).addClass('is-invalid').removeClass('is-valid');
            $(element).closest('.form-group').find('.input-group').addClass('is-invalid');
        },
        unhighlight: function (element) {
            $(element).removeClass('is-invalid').addClass('is-valid');
            $(element).closest('.form-group').find('.input-group').removeClass('is-invalid');
        },
        errorPlacement: function (error, element) {
            const fieldName = element.attr('name');
            $(`#${fieldName}-error`).html(error.html());
        }
    });
}

function togglePasswordVisibility(fieldId) {
    const field = $('#' + fieldId);
    const eye = $('#' + fieldId + '-eye');

    if (field.attr('type') === 'password') {
        field.attr('type', 'text');
        eye.removeClass('bi-eye').addClass('bi-eye-slash');
    } else {
        field.attr('type', 'password');
        eye.removeClass('bi-eye-slash').addClass('bi-eye');
    }
}


function showMessage(message, type = "success") {
    const container = $("#messageContainer");
    const content = $("#messageContent");

    content.removeClass('alert-success alert-danger alert-warning alert-info')
        .addClass(`alert-${type}`);
    content.html(`<i class="bi ${type === 'success' ? 'check-circle' : 'exclamation-triangle'}-fill me-2"></i> ${message}`);

    container.removeClass("d-none");

    // Auto-hide after 5 seconds
    setTimeout(() => {
        container.addClass("d-none");
    }, 5000);
}

// Handle password enter key
function handleEnterKey(e) {
    if (e.key === 'Enter') {
        submitLogin();
    }
}