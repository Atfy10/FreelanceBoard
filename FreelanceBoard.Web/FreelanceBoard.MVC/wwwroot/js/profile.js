// profile.js

let originalValues = {};
let isEditMode = false;

document.addEventListener('DOMContentLoaded', function () {
    storeOriginalValues();
    setupPhoneNumberValidation();

    // show message if stored in localStorage
    const msg = localStorage.getItem("successMessage");
    if (msg) {
        showMessage(msg);
        localStorage.removeItem("successMessage");
    }

    // 🔹 Toggle Edit Mode
    const editBtn = document.getElementById("editModeBtn");
    if (editBtn) {
        editBtn.addEventListener("click", toggleEditMode);
    }

    // 🔹 Save Profile
    const saveBtn = document.getElementById("saveBtn");
    if (saveBtn) {
        saveBtn.addEventListener("click", saveProfile);
    }

    // 🔹 Profile Picture Upload
    const profileUploadTrigger = document.getElementById("profileUploadTrigger");
    const profilePictureInput = document.getElementById("profilePictureInput");
    if (profileUploadTrigger && profilePictureInput) {
        profileUploadTrigger.addEventListener("click", () => profilePictureInput.click());
        profilePictureInput.addEventListener("change", handleProfilePictureChange);
    }

    // 🔹 Skills
    const addSkillBtn = document.getElementById("addSkillBtn");
    if (addSkillBtn) {
        addSkillBtn.addEventListener("click", openAddSkillModal);
    }
    document.querySelectorAll(".skill-remove").forEach(btn => {
        btn.addEventListener("click", () => removeSkill(btn.dataset.skill, btn));
    });

    // 🔹 Save Skill
    const saveSkillBtn = document.getElementById("saveSkillBtn");
    if (saveSkillBtn) {
        saveSkillBtn.addEventListener("click", saveSkill);
    }

    // 🔹 Projects
    const addProjectBtn = document.getElementById("addProjectBtn");
    if (addProjectBtn) {
        addProjectBtn.addEventListener("click", openAddProjectModal);
    }
    document.querySelectorAll(".project-remove").forEach(btn => {
        btn.addEventListener("click", () => removeProject(btn.dataset.projectId, btn));
    });

    // 🔹 Save Project
    const saveProjectBtn = document.getElementById("saveProjectBtn");
    if (saveProjectBtn) {
        saveProjectBtn.addEventListener("click", saveProject);
    }
    
});

// ----------------- Utility & Actions -----------------

function storeOriginalValues() {
    originalValues = {
        username: document.getElementById('username').value,
        phoneNumber: document.getElementById('phoneNumber').value,
        bio: document.getElementById('bio').value
    };
}

function showMessage(message, type = "success") {
    const container = document.getElementById("messageContainer");
    const content = document.getElementById("messageContent");

    content.className = "alert alert-" + type;
    content.textContent = message;

    container.classList.remove("d-none");

    window.scrollTo({ top: 0, behavior: "smooth" });

    setTimeout(() => {
        container.classList.add("d-none");
    }, 6000);
}

function setupPhoneNumberValidation() {
    const phoneInput = document.getElementById('phoneNumber');
    if (!phoneInput) return;
    phoneInput.addEventListener('input', function () {
        this.value = this.value.replace(/[^0-9]/g, '');
    });
}

function toggleEditMode() {
    isEditMode = !isEditMode;
    const editBtnText = document.getElementById('edit-btn-text');
    const saveBtn = document.getElementById('saveBtn');
    const inputFields = document.querySelectorAll('input, textarea');
    const skillRemoveBtns = document.querySelectorAll('.skill-remove');
    const addSkillBtn = document.getElementById('addSkillBtn');
    const addProjectBtn = document.getElementById('addProjectBtn');
    const projectRemoveBtns = document.querySelectorAll('.project-remove');

    if (isEditMode) {
        editBtnText.textContent = 'Cancel Edit';
        saveBtn.classList.remove("d-none");
        if (addSkillBtn) addSkillBtn.classList.remove("d-none");
        if (addProjectBtn) addProjectBtn.classList.remove("d-none");

        inputFields.forEach(field => {
            field.removeAttribute('readonly');
            field.classList.remove('readonly-field');
        });

        skillRemoveBtns.forEach(btn => btn.classList.remove("d-none"));
        projectRemoveBtns.forEach(btn => btn.classList.remove("d-none"));
    } else {
        editBtnText.textContent = 'Edit Profile';
        saveBtn.classList.add("d-none");
        if (addSkillBtn) addSkillBtn.classList.add("d-none");
        if (addProjectBtn) addProjectBtn.classList.add("d-none");

        document.getElementById('username').value = originalValues.username;
        document.getElementById('phoneNumber').value = originalValues.phoneNumber;
        document.getElementById('bio').value = originalValues.bio;

        inputFields.forEach(field => {
            field.setAttribute('readonly', true);
            field.classList.add('readonly-field');
        });

        skillRemoveBtns.forEach(btn => btn.classList.add("d-none"));
        projectRemoveBtns.forEach(btn => btn.classList.add("d-none"));
    }
}

async function saveProfile() {
    const profileData = {
        userName: document.getElementById('username').value.trim(),
        phoneNumber: document.getElementById('phoneNumber').value.trim(),
        bio: document.getElementById('bio').value.trim()
    };

    // Validate inputs
    if (!profileData.userName) {
        showMessage('Username is required');
        return;
    }

    if (!profileData.phoneNumber || profileData.phoneNumber.length < 10 || profileData.phoneNumber.length > 15) {
        showMessage('Please enter a valid phone number (10-15 digits)');
        return;
    }

    try {
        // Show loading state
        const saveBtn = document.getElementById('saveBtn');
        saveBtn.innerHTML = '<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Saving...';
        saveBtn.disabled = true;

        const response = await fetch("/User/UpdateProfile", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(profileData)
        });

        if (response.ok) {
            // Success - refresh the page to get updated data
            location.reload();
        } else {
            const result = await response.json();
            if (result.error) {
                showMessage(result.error, "danger");
            } else {
                showMessage("❌ Failed to update profile", "danger");
            }
        }
    } catch (error) {
        console.error('Error:', error);
        showMessage('An error occurred while updating profile');
    } finally {
        // Reset save button
        const saveBtn = document.getElementById('saveBtn');
        if (saveBtn) {
            saveBtn.innerHTML = '<i class="bi-check-circle me-2"></i> Save Changes';
            saveBtn.disabled = false;
        }
    }
}

async function handleProfilePictureChange(event) {
    const file = event.target.files[0];
    if (!file) return;

    const formData = new FormData();
    formData.append("ProfilePicture", file);

    const token = sessionStorage.getItem("token");
    console.log("Uploading profile picture with token:", token);

    try {
        const response = await fetch("https://localhost:7029/api/User/change-profile-picture", {
            method: "POST",
            body: formData,
            headers: {
                Authorization: `Bearer ${token}`
            }
        });
        console.log("Form data:", formData);

        const result = await response.json();
        console.log("Response:", result);

        if (response.ok && result.isSuccess) {
            // ✅ Update image on screen
            document.getElementById("profilePicture").src = "https://localhost:7029" + result.data;
            showMessage("Profile picture updated successfully!");
        } else {
            showMessage("Failed to update profile picture: " + result.message);
        }
    } catch {
        showMessage("Error uploading image.");
    }
}

async function openAddSkillModal() {
    const modal = new bootstrap.Modal(document.getElementById("addSkillModal"));

    try {
        const response = await fetch("https://localhost:7029/api/Skill/get-all");
        const result = await response.json();

        if (result.isSuccess && Array.isArray(result.data)) {
            const select = document.getElementById("skillSelect");
            select.innerHTML = '<option value="">-- Choose a skill --</option>';


            // Filter out skills the user already has
            const filteredSkills = result.data.filter(skill => !userSkills.includes(skill.name));

            // Populate only the filtered skills
            filteredSkills.forEach(skill => {
                const option = document.createElement("option");
                option.value = skill.id;
                option.textContent = skill.name;
                select.appendChild(option);
            });
        } else {
            showMessage("❌ Failed to load skills");
        }
    } catch (error) {
        console.error("Error fetching skills:", error);
        showMessage("❌ Error loading skills");
    }

    modal.show();
}

async function saveSkill() {
    console.log("saveSkill function called");
    const skillSelect = document.getElementById("skillSelect");
    const selectedId = skillSelect.value;
    const selectedName = skillSelect.options[skillSelect.selectedIndex]?.text;

    if (!selectedId) {
        showMessage("⚠️ Please select a skill.");
        return;
    }

    const response = await fetch("/User/AddSkill", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ skillName: selectedName })
    });

    if (response.ok) {
        bootstrap.Modal.getInstance(document.getElementById('addSkillModal')).hide();
        localStorage.setItem("successMessage", `✅ Skill ${selectedName} added successfully!`);
        location.reload();
    } else {
        showMessage("❌ Failed to add skill");
    }
}
function openAddProjectModal() {
    const modal = new bootstrap.Modal(document.getElementById('addProjectModal'));
    modal.show();
}

async function saveProject() {
    const projectData = {
        title: document.getElementById("projectTitle").value.trim(),
        description: document.getElementById("projectDescription").value.trim(),
        attachments: document.getElementById("projectAttachments").value.trim()
    };

    try {
        const response = await fetch("/User/AddProject", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(projectData)
        });

        if (!response.ok) {
            const result = await response.json().catch(() => null);
            if (result?.error) {
                showMessage(`❌ ${result.error}`, "danger");
            } else {
                showMessage("❌ Failed to add project", "danger");
            }
            return;
        }

        const result = await response.json();
        bootstrap.Modal.getInstance(document.getElementById('addProjectModal')).hide();

        localStorage.setItem("successMessage", `✅ Project "${projectData.title}" added successfully!`);
        setTimeout(() => {
            location.reload();
        }, 2000);

    } catch (error) {
        console.error("Fetch error:", error);
        showMessage("❌ Network error while saving project", "danger");
    }
}

async function removeProject(projectId, button) {
    if (!confirm('Are you sure you want to delete this project?')) return;

    try {
        const response = await fetch(`/User/DeleteProject?projectId=${projectId}`, {
            method: "DELETE",
            headers: { "Content-Type": "application/json" }
        });

        if (response.ok) {
            // Remove project card from DOM
            button.closest('.project-card').remove();
            showMessage('✅ Project deleted successfully!', 'success');
        } else {
            const error = await response.text();
            showMessage('❌ Failed to delete project: ' + error);
        }
    } catch (error) {
        console.error('Error:', error);
        showMessage('❌ Error deleting project');
    }
}      

async function removeSkill(skillName, button) {
    if (!confirm(`Are you sure you want to remove "${skillName}"?`)) return;

    const response = await fetch("/User/RemoveSkill", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ skillName: skillName }) // send skill name to backend
    });

    if (response.ok) {
        // Store message in localStorage to show after reload
        localStorage.setItem("successMessage", `✅ Skill "${skillName}" removed successfully!`);
        location.reload();
    } else {
        const result = await response.json().catch(() => null);
        if (result?.error) {
            showMessage(`❌ ${result.error}`, "danger");
        } else {
            showMessage(`❌ Failed to remove skill "${skillName}"`, "danger");
        }
    }
}