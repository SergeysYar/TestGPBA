document.addEventListener("DOMContentLoaded", () => {

    // Функция для загрузки популярных поставщиков
    function loadPopularSuppliers() {
        fetch(`$/api/suppliers/popular`)
            .then(response => response.json())
            .then(data => {
                const supplierList = document.getElementById("supplier-list");
                supplierList.innerHTML = "";
                data.forEach(supplier => {
                    const li = document.createElement("li");
                    li.textContent = `${supplier.name} - ${supplier.offerCount} offers`;
                    supplierList.appendChild(li);
                });
            })
            .catch(error => console.error("Error loading popular suppliers:", error));
    }

    // Функция для поиска офферов
    function searchOffers(query) {
        fetch(`${apiUrl}/api/offers/search?query=${encodeURIComponent(query)}`)
            .then(response => response.json())
            .then(data => {
                const offerList = document.getElementById("offer-list");
                offerList.innerHTML = "";
                data.offers.forEach(offer => {
                    const li = document.createElement("li");
                    li.textContent = `Brand: ${offer.brand}, Model: ${offer.model}, Supplier: ${offer.supplier.name}, Date: ${new Date(offer.registrationDate).toLocaleString()}`;
                    offerList.appendChild(li);
                });
                document.getElementById("offer-count").textContent = `Total Offers: ${data.totalCount}`;
            })
            .catch(error => console.error("Error searching offers:", error));
    }

    // Загрузка популярных поставщиков при загрузке страницы
    loadPopularSuppliers();

    // Обработчик для кнопки поиска
    document.getElementById("search-button").addEventListener("click", () => {
        const query = document.getElementById("search-input").value;
        searchOffers(query);
    });
});

