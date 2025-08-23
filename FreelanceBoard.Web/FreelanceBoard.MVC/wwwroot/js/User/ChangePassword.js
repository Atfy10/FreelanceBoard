// changePassword.js - Refactored with jQuery

$(document).ready(function () {
    initializePage();
    setupEventListeners();
    setupValidation();
});

function initializePage() {
    // Set user ID from localStorage
    const userId = localStorage.getItem("userId");
    if (userId) {
        $("#UserId").val(userId);
    } 
}

function setupEventListeners() {
    // Password toggle buttons
    $(document).on('click', '.password-toggle-btn', function () {
        const field = $(this).data('field');
        togglePasswordVisibility(field);
    });

    // Password strength check
    $('#NewPassword').on('input', checkPasswordStrength);

    // Password match check
    $('#NewPassword, #ConfirmNewPassword').on('input', checkPasswordMatch);

    // Current password input
    $('#CurrentPassword').on('input', checkPasswordMatch);

    // Form submission
    $('#changePasswordForm').on('submit', function (e) {
        e.preventDefault();
        submitPasswordChange();
    });
}

function setupValidation() {
    // jQuery Validation setup
    $('#changePasswordForm').validate({
        rules: {
            CurrentPassword: {
                required: true,
                minlength: 1
            },
            NewPassword: {
                required: true,
                minlength: 6,
                strongPassword: true
            },
            ConfirmNewPassword: {
                required: true,
                equalTo: "#NewPassword"
            }
        },
        messages: {
            CurrentPassword: {
                required: "Please enter your current password",
            },
            NewPassword: {
                required: "Please enter a new password",
                minlength: "Password must be at least 6 characters"
            },
            ConfirmNewPassword: {
                required: "Please confirm your new password",
                equalTo: "Passwords do not match"
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

    // Custom validation method for strong password
    $.validator.addMethod("strongPassword", function (value, element) {
        return this.optional(element) ||
            /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*(),.?":{}|<>]).{6,}$/.test(value);
    }, "Password must contain uppercase, lowercase, number, and special character");
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

function checkPasswordStrength() {
    const password = $('#NewPassword').val();
    const strengthBar = $('#passwordStrengthBar');

    const requirements = {
        length: password.length >= 6,
        uppercase: /[A-Z]/.test(password),
        lowercase: /[a-z]/.test(password),
        number: /\d/.test(password),
        special: /[!@#$%^&*(),.?":{}|<>]/.test(password)
    };

    // Update requirement icons
    Object.entries(requirements).forEach(([key, passed]) => {
        const item = $('#req-' + key);
        const icon = item.find('i');

        if (passed) {
            item.addClass('met');
            icon.removeClass('bi-x-circle').addClass('bi-check-circle-fill');
        } else {
            item.removeClass('met');
            icon.removeClass('bi-check-circle-fill').addClass('bi-x-circle');
        }
    });

    // Update strength bar
    const passedCount = Object.values(requirements).filter(Boolean).length;
    const strengthClasses = ['strength-weak', 'strength-fair', 'strength-good', 'strength-strong'];
    strengthBar.removeClass().addClass('password-strength-bar ' + (strengthClasses[passedCount - 1] || ''));
}

async function submitPasswordChange() {
    if (!$('#changePasswordForm').valid()) return;

    const formData = {
        UserId: $('#UserId').val(),
        CurrentPassword: $('#CurrentPassword').val(),
        NewPassword: $('#NewPassword').val(),
        ConfirmNewPassword: $('#ConfirmNewPassword').val()
    };

    try {
        // Show loading state
        $('#changePasswordBtn').html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Updating...')
            .addClass('btn-loading')
            .prop('disabled', true);

        const response = await fetch("/User/ChangePassword", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val()
            },
            body: JSON.stringify(formData)
        });

        const result = await response.json();

        if (response.ok) {
            if (result.success) {
                showMessage("✅ Password changed successfully!", "success");
                $('#changePasswordForm')[0].reset();
                resetPasswordUI();

                // Redirect to profile after success
                setTimeout(() => {
                    window.location.href = "/User/Profile";
                }, 2000);
            } else {
                showMessage("❌ " + (result.message || "Failed to change password"), "danger");
            }
        } else {
            showMessage("❌ " + (result.message || "Server error occurred"), "danger");
        }
    } catch (error) {
        console.error("Error:", error);
        showMessage("❌ Network error occurred", "danger");
    } finally {
        $('#changePasswordBtn').html('<i class="bi-shield-check me-2"></i> Update Password')
            .removeClass('btn-loading')
            .prop('disabled', false);
    }
}

function resetPasswordUI() {
    // Reset strength indicators
    $('#passwordStrengthBar').removeClass('strength-weak strength-fair strength-good strength-strong');
    $('.requirement').removeClass('met').find('i').removeClass('bi-check-circle-fill').addClass('bi-x-circle');
    $('#passwordMatchMessage').html('');
}

function showMessage(message, type = "success") {
    const container = $("#messageContainer");
    const content = $("#messageContent");

    content.removeClass('alert-success alert-danger alert-warning')
        .addClass(`alert-${type}`);
    content.html(message);

    container.removeClass("d-none");

    $('html, body').animate({ scrollTop: 0 }, 'slow');

    setTimeout(() => {
        container.addClass("d-none");
    }, 5000);
}