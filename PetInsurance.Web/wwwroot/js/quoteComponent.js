const { createApp, ref, watch, onMounted } = Vue;

const QuoteComponent = {
    template: `
     <fieldset class="mb-4 border rounded p-3" v-if="formFields.age.value > 0">
         <legend>Estimated Monthly Premium</legend>
         <label>
             <div v-if="loading">Calculating...</div>
             <div v-else>\${{ premium.toFixed(2) }}</div>
         </label>
     </fieldset>
    `,
    setup() {
        const premium = ref(0);
        const loading = ref(false);
        const error = ref(null);
        const token = ref(null);
        const apiBaseUrl = document.getElementById('quote-app').dataset.apiUrl;

        if (!apiBaseUrl) {
            error.value = 'API URL configuration error';
        }

        const formFields = {
            breed: ref('Other'),
            age: ref(0),
            coverLevel: ref('Standard')
        };

        const loginAndGetToken = async () => {
            try {
                const res = await fetch(`${apiBaseUrl}/api/auth/login`, {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ username: 'test', password: 'pass' })
                });

                if (!res.ok) {
                    const errorData = await res.text();
                    throw new Error(`Login failed: ${errorData}`);
                }

                const data = await res.json();
                if (!data.token) {
                    throw new Error('No token received from login response');
                }
                token.value = data.token;
            } catch (err) {
                error.value = err.message;
            }
        };

        const fetchPremium = async () => {
            if (!token.value) {
                await loginAndGetToken();
                if (!token.value) {
                    error.value = "Authorization failed.";
                    return;
                }
            }

            loading.value = true;
            error.value = null;

            try {
                const response = await fetch(`${apiBaseUrl}/api/quote`, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${token.value}`
                    },
                    body: JSON.stringify({
                        coverLevel: formFields.coverLevel.value,
                        age: parseFloat(formFields.age.value),
                        breed: formFields.breed.value
                    })
                });

                if (!response.ok) {
                    const errorData = await response.text();
                    throw new Error(`Error fetching quote: ${errorData}`);
                }

                const data = await response.json();
                premium.value = data.premium;
            } catch (err) {
                error.value = err.message;
            } finally {
                loading.value = false;
            }
        };

        const syncFormFields = () => {
            formFields.breed.value = document.querySelector('[name="Pet.Breed"]').value;
            formFields.age.value = document.querySelector('[name="Pet.Age"]').value;
            formFields.coverLevel.value = document.querySelector('[name="CoverLevel"]').value;
        };

        Object.values(formFields).forEach(field => watch(field, fetchPremium));

        onMounted(() => {
            syncFormFields();
            fetchPremium();

            const relevantFields = ['Pet.Breed', 'Pet.Age', 'CoverLevel'];
            relevantFields.forEach(fieldName => {
                const input = document.querySelector(`[name="${fieldName}"]`);
                if (input) {
                    input.addEventListener('input', syncFormFields);
                    input.addEventListener('change', syncFormFields);
                }
            });
        });

        return { premium, loading, error, formFields };
    }
};

createApp(QuoteComponent).component('quote-component', QuoteComponent).mount('#quote-app');