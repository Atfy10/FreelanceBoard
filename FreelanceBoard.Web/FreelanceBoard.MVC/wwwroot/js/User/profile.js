let originalValues = {};
let isEditMode = false;

$(document).ready(function () {
    initializePage();
    setupEventListeners();
    setupValidation();
});

// ----------------- Initialization -----------------

function initializePage() {
    storeOriginalValues();

    // Show message if stored in localStorage
    const msg = localStorage.getItem("successMessage");
    if (msg) {
        showMessage(msg, "success");
        localStorage.removeItem("successMessage");
    }
}

function storeOriginalValues() {
    originalValues = {
        username: $('#username').val(),
        phoneNumber: $('#phoneNumber').val(),
        bio: $('#bio').val()
    };
}

// ----------------- Event Listeners -----------------

function setupEventListeners() {
    // Toggle Edit Mode
    $('#editModeBtn').on('click', toggleEditMode);

    // Save Profile
    $('#saveBtn').on('click', function (e) {
        e.preventDefault();
        saveProfile();
    });

    // Profile Picture Upload
    $('#profileUploadTrigger').on('click', function () {
        $('#profilePictureInput').click();
    });

    $('#profilePictureInput').on('change', handleProfilePictureChange);

    // Skills
    $('#addSkillBtn').on('click', openAddSkillModal);
    $(document).on('click', '.skill-remove', function () {
        removeSkill($(this).data('skill'), $(this));
    });
    $('#saveSkillBtn').on('click', saveSkill)

    // Projects
    $('#addProjectBtn').on('click', openAddProjectModal);
    $(document).on('click', '.project-remove', function () {
        removeProject($(this).data('project-id'), $(this));
    });
    $('#saveProjectBtn').on('click', saveProject);

    // Project attachment click
    $(document).on('click', '.project-attachment', function () {
        const url = $(this).data('url');
        const isLink = $(this).data('islink');

        if (isLink) {
            window.open(url, '_blank');
        } else {
            // Handle file download/view logic here
            console.log('File attachment clicked:', url);
        }
    });
}

// ----------------- Validation Setup -----------------

function setupValidation() {
    // Phone number validation
    $('#phoneNumber').on('input', function () {
        $(this).val($(this).val().replace(/[^0-9]/g, ''));
    });

    // Setup project form validation
    $('#addProjectForm').validate({
        rules: {
            projectTitle: {
                required: true,
                minlength: 3,
                maxlength: 100
            },
            projectDescription: {
                required: true,
                minlength: 10,
                maxlength: 500
            }
        },
        messages: {
            projectTitle: {
                required: "Please enter a project title",
                minlength: "Title must be at least 3 characters",
                maxlength: "Title cannot exceed 100 characters"
            },
            projectDescription: {
                required: "Please enter a project description",
                minlength: "Description must be at least 10 characters",
                maxlength: "Description cannot exceed 500 characters"
            }
        },
        errorElement: 'div',
        errorClass: 'invalid-feedback',
        highlight: function (element) {
            $(element).addClass('is-invalid').removeClass('is-valid');
        },
        unhighlight: function (element) {
            $(element).addClass('is-valid').removeClass('is-invalid');
        },
        errorPlacement: function (error, element) {
            error.insertAfter(element);
        }
    });

    // Setup skill form validation
    $('#addSkillForm').validate({
        rules: {
            skillSelect: {
                required: true
            }
        },
        messages: {
            skillSelect: {
                required: "Please select a skill"
            }
        },
        errorElement: 'div',
        errorClass: 'invalid-feedback',
        highlight: function (element) {
            $(element).addClass('is-invalid').removeClass('is-valid');
        },
        unhighlight: function (element) {
            $(element).addClass('is-valid').removeClass('is-invalid');
        },
        errorPlacement: function (error, element) {
            error.insertAfter(element);
        }
    });
}

// ----------------- Utility Functions -----------------

function showMessage(message, type = "success") {
    const container = $("#messageContainer");
    const content = $("#messageContent");

    content.removeClass('alert-success alert-danger alert-warning')
        .addClass(`alert-${type}`);
    content.text(message);

    container.removeClass("d-none");

    $('html, body').animate({ scrollTop: 0 }, 'slow');

    setTimeout(() => {
        container.addClass("d-none");
    }, 6000);
}

function toggleEditMode() {
    isEditMode = !isEditMode;
    const editBtnText = $('#edit-btn-text');
    const saveBtn = $('#saveBtn');
    const inputFields = $('input, textarea');
    const skillRemoveBtns = $('.skill-remove');
    const addSkillBtn = $('#addSkillBtn');
    const addProjectBtn = $('#addProjectBtn');
    const projectRemoveBtns = $('.project-remove');

    if (isEditMode) {
        editBtnText.text('Cancel Edit');
        saveBtn.removeClass("d-none");
        addSkillBtn.removeClass("d-none");
        addProjectBtn.removeClass("d-none");

        inputFields.prop('readonly', false).removeClass('readonly-field');
        skillRemoveBtns.removeClass("d-none");
        projectRemoveBtns.removeClass("d-none");

        // Add editing class to skill tags
        $('.skill-tag').addClass('editing');
    } else {
        editBtnText.text('Edit Profile');
        saveBtn.addClass("d-none");
        addSkillBtn.addClass("d-none");
        addProjectBtn.addClass("d-none");

        // Restore original values
        $('#username').val(originalValues.username);
        $('#phoneNumber').val(originalValues.phoneNumber);
        $('#bio').val(originalValues.bio);

        inputFields.prop('readonly', true).addClass('readonly-field');
        skillRemoveBtns.addClass("d-none");
        projectRemoveBtns.addClass("d-none");

        // Remove editing class from skill tags
        $('.skill-tag').removeClass('editing');
    }
}

// ----------------- API Functions -----------------

async function saveProfile() {
    const profileData = {
        userName: $('#username').val().trim(),
        phoneNumber: $('#phoneNumber').val().trim(),
        bio: $('#bio').val().trim()
    };

    // Client-side validation
    if (!profileData.userName) {
        showMessage('Username is required', 'danger');
        $('#username').addClass('is-invalid');
        return;
    }

    if (!profileData.phoneNumber || profileData.phoneNumber.length < 10 || profileData.phoneNumber.length > 15) {
        showMessage('Please enter a valid phone number (10-15 digits)', 'danger');
        $('#phoneNumber').addClass('is-invalid');
        return;
    }

    try {
        // Show loading state
        const saveBtn = $('#saveBtn');
        saveBtn.html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Saving...')
            .addClass('btn-loading')
            .prop('disabled', true);

        const response = await fetch("/User/UpdateProfile", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val()
            },
            body: JSON.stringify(profileData)
        });

        if (response.ok) {
            showMessage('✅ Profile updated successfully!', 'success');
            setTimeout(() => location.reload(), 1500);
        } else {
            const result = await response.json();
            showMessage(result.error || "❌ Failed to update profile", "danger");
        }
    } catch (error) {
        console.error('Error:', error);
        showMessage('❌ An error occurred while updating profile', 'danger');
    } finally {
        $('#saveBtn').html('<i class="bi-check-circle me-2"></i> Save Changes')
            .removeClass('btn-loading')
            .prop('disabled', false);
    }
}

async function handleProfilePictureChange(event) {
    const file = event.target.files[0];
    if (!file) return;

    // Validate file type and size
    const validTypes = ['image/jpeg', 'image/png', 'image/gif'];
    const maxSize = 5 * 1024 * 1024; // 5MB

    if (!validTypes.includes(file.type)) {
        showMessage('Please select a valid image file (JPEG, PNG, GIF)', 'danger');
        return;
    }

    if (file.size > maxSize) {
        showMessage('Image size must be less than 5MB', 'danger');
        return;
    }

    const formData = new FormData();
    formData.append("ProfilePicture", file);

    const token = sessionStorage.getItem("token");

    try {
        $('#profileUploadTrigger').addClass('btn-loading');

        const response = await fetch("https://localhost:7029/api/User/change-profile-picture", {
            method: "POST",
            body: formData,
            headers: {
                "Authorization": `Bearer ${token}`
            }
        });

        const result = await response.json();

        if (response.ok && result.isSuccess) {
            $('#profilePicture').attr("src", "https://localhost:7029" + result.data + "?t=" + new Date().getTime());
            showMessage("✅ Profile picture updated successfully!", "success");
        } else {
            showMessage("❌ Failed to update profile picture: " + (result.message || "Unknown error"), "danger");
        }
    } catch (error) {
        console.error("Upload error:", error);
        showMessage("❌ Error uploading image", "danger");
    } finally {
        $('#profileUploadTrigger').removeClass('btn-loading');
        $('#profilePictureInput').val('');
    }
}

async function openAddSkillModal() {
    const modal = new bootstrap.Modal($("#addSkillModal")[0]);

    try {
        const response = await fetch("https://localhost:7029/api/Skill/get-all");
        const result = await response.json();

        if (result.isSuccess && Array.isArray(result.data)) {
            const select = $("#skillSelect");
            select.empty().append('<option value="">-- Choose a skill --</option>');

            const filteredSkills = result.data.filter(skill => !userSkills.includes(skill.name));

            filteredSkills.forEach(skill => {
                select.append($('<option>', {
                    value: skill.id,
                    text: skill.name
                }));
            });
        } else {
            showMessage("❌ Failed to load skills", "danger");
        }
    } catch (error) {
        console.error("Error fetching skills:", error);
        showMessage("❌ Error loading skills", "danger");
    }

    modal.show();
}

async function saveSkill() {
    if (!$('#addSkillForm').valid()) return;

    const skillSelect = $("#skillSelect");
    const selectedId = skillSelect.val();
    const selectedName = skillSelect.find('option:selected').text();

    try {
        $('#saveSkillBtn').addClass('btn-loading').prop('disabled', true);

        const response = await fetch("/User/AddSkill", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val()
            },
            body: JSON.stringify({ skillName: selectedName })
        });

        if (response.ok) {
            bootstrap.Modal.getInstance($('#addSkillModal')[0]).hide();
            localStorage.setItem("successMessage", `✅ Skill "${selectedName}" added successfully!`);
            location.reload();
        } else {
            const result = await response.json();
            showMessage(result.error || "❌ Failed to add skill", "danger");
        }
    } catch (error) {
        console.error("Error:", error);
        showMessage("❌ Network error while adding skill", "danger");
    } finally {
        $('#saveSkillBtn').removeClass('btn-loading').prop('disabled', false);
    }
}

function openAddProjectModal() {
    $('#addProjectForm')[0].reset();
    $('#addProjectForm').validate().resetForm();

    const modal = new bootstrap.Modal($('#addProjectModal')[0]);
    modal.show();
}

async function saveProject() {
    if (!$('#addProjectForm').valid()) return;

    const projectData = {
        title: $("#projectTitle").val().trim(),
        description: $("#projectDescription").val().trim(),
        attachments: $("#projectAttachments").val().trim()
    };

    try {
        $('#saveProjectBtn').addClass('btn-loading').prop('disabled', true);

        const response = await fetch("/User/AddProject", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val()
            },
            body: JSON.stringify(projectData)
        });

        if (response.ok) {
            bootstrap.Modal.getInstance($('#addProjectModal')[0]).hide();
            localStorage.setItem("successMessage", `✅ Project "${projectData.title}" added successfully!`);
            setTimeout(() => location.reload(), 1000);
        } else {
            const result = await response.json();
            showMessage(result.error || "❌ Failed to add project", "danger");
        }
    } catch (error) {
        console.error("Error:", error);
        showMessage("❌ Network error while saving project", "danger");
    } finally {
        $('#saveProjectBtn').removeClass('btn-loading').prop('disabled', false);
    }
}

async function removeProject(projectId, button) {
    if (!confirm('Are you sure you want to delete this project?')) return;

    try {
        const response = await fetch(`/User/DeleteProject?projectId=${projectId}`, {
            method: "DELETE",
            headers: {
                "Content-Type": "application/json",
                "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val()
            }
        });

        if (response.ok) {
            button.closest('.project-card').fadeOut(300, function () {
                $(this).remove();
                showMessage('✅ Project deleted successfully!', 'success');
            });
        } else {
            const error = await response.text();
            showMessage('❌ Failed to delete project: ' + error, 'danger');
        }
    } catch (error) {
        console.error('Error:', error);
        showMessage('❌ Error deleting project', 'danger');
    }
}

async function removeSkill(skillName, button) {
    if (!confirm(`Are you sure you want to remove "${skillName}"?`)) return;

    try {
        const response = await fetch("/User/RemoveSkill", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val()
            },
            body: JSON.stringify({ skillName: skillName })
        });

        if (response.ok) {
            localStorage.setItem("successMessage", `✅ Skill "${skillName}" removed successfully!`);
            location.reload();
        } else {
            const result = await response.json();
            showMessage(result.error || `❌ Failed to remove skill "${skillName}"`, "danger");
        }
    } catch (error) {
        console.error("Error:", error);
        showMessage("❌ Network error while removing skill", "danger");
    }
}