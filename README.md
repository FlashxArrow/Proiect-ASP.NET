# GraphicNovelShop

Acest proiect reprezintă un magazin online de graphic novels, unde utilizatorii pot cumpăra benzi desenate, vinde propriile produse (daca sunt selleri) și administra comenzile (daca sunt admini).

## **Diagrama bazei de date**
![image](https://github.com/user-attachments/assets/4581dea4-4c54-45b5-8d59-444c3b32e10e)


## **Structura bazei de date**
Baza de date este concepută pentru a gestiona informații despre utilizatori, autori, francize, benzi desenate, comenzi și roluri.

### **Tabelul Users**
Acest tabel stochează informații despre utilizatori.
- `id_user` (cheie primară) - Identificator unic al utilizatorului.
- `name` - Numele utilizatorului.
- `email` - Adresa de email a utilizatorului.
- `password` - Parola utilizatorului (hashuită).
- `role` - Rolul utilizatorului (buyer, seller, admin).
- `budget` - Bugetul disponibil al utilizatorului.
- `phone_number` - Numărul de telefon al utilizatorului.
- `address` - Adresa utilizatorului.

### **Tabelul Authors**
Acest tabel conține informații despre autori.
- `id_author` (cheie primară) - Identificator unic pentru autor.
- `author_name` - Numele autorului.
- `date_birth` - Data nașterii.
- `phone_number` - Numărul de telefon.
- `address` - Adresa autorului.
- `salary` - Salariul autorului.
- `age` - Vârsta autorului.

### **Tabelul Franchises**
Acest tabel conține informații despre francizele de benzi desenate.
- `id_franchises` (cheie primară) - Identificator unic al francizei.
- `franchises_name` - Numele francizei.
- `headquarters` - Sediul francizei.
- `budget` - Bugetul francizei.
- `date_foundation` - Data fondării.
- `id_author` (cheie externă) - Referință către autorul principal al francizei.
- `average_rating` - Ratingul mediu al francizei.

### **Tabelul Comics**
Acest tabel conține informații despre benzile desenate disponibile.
- `id_comic` (cheie primară) - Identificator unic pentru fiecare bandă desenată.
- `id_author` (cheie externă) - Referință către autor.
- `id_franchises` (cheie externă) - Referință către franciză.
- `comic_name` - Numele benzii desenate.
- `rating` - Ratingul benzii desenate.
- `price` - Prețul benzii desenate.
- `stock` - Stocul disponibil.
- `short_description` - Descrierea benzii desenate.
- `image_url` - URL-ul imaginii produsului.

### **Tabelul Orders**
Acest tabel conține informații despre comenzile plasate de utilizatori.
- `id_order` (cheie primară) - Identificator unic al comenzii.
- `id_client` (cheie externă) - Utilizatorul care a plasat comanda.
- `id_comic` (cheie externă) - Banda desenată comandată.
- `quantity` - Cantitatea comandată.
- `order_date` - Data plasării comenzii.
- `shipping_address` - Adresa de livrare.
- `payment_method` - Metoda de plată utilizată.
- `order_status` - Starea comenzii (procesare, expediată, livrată).
- `shipping_method` - Metoda de livrare utilizată.
- `transaction_type` - Indica dacă este o vânzare pentru seller sau o achiziție pentru buyer.

## **Roluri în aplicație**
### **Buyer (Cumpărător)**
- Vizualizează produsele disponibile.
- Adaugă produse în coș și finalizează comenzi.
- Poate aplica pentru a deveni seller.
- Vizualizează comenzile plasate.

### **Seller (Vânzător)**
- Poate vinde produse.
- Vizualizează comenzile plasate și comenzile efectuate către alți utilizatori.
- Poate modifica sau șterge propriile produse.

### **Admin**
- Are acces complet la comenzile utilizatorilor.
- Poate adăuga, edita sau șterge produse.
- Poate aproba cererile utilizatorilor de a deveni selleri.

## **Fluxul de funcționare**
### **Pentru Buyer**
1. **Autentificare și înregistrare:** Poate crea un cont și se autentifică.
2. **Vizualizare produse:** Poate explora catalogul de benzi desenate.
3. **Adăugare în coș:** Poate selecta produse pentru achiziție.
4. **Finalizare comandă:** Introduce detaliile de livrare și finalizează plata.
5. **Vizualizare comenzi:** Poate vedea istoricul comenzilor.

### **Pentru Seller**
1. **Autentificare ca Seller:** Poate fi aprobat de un admin.
2. **Adăugare produse:** Poate lista produse pentru vânzare.
3. **Gestionare comenzi:** Poate monitoriza comenzile primite de la cumpărători.

### **Pentru Admin**
1. **Autentificare:** Se conectează cu permisiuni de admin.
2. **Gestionare utilizatori:** Poate aproba selleri și gestiona conturi.
3. **Administrare produse:** Poate edita și șterge produse.
4. **Monitorizare comenzi:** Poate anula sau verifica comenzi.

## **Tehnologii utilizate**
- **ASP.NET Core MVC** - Backend
- **Entity Framework Core** - ORM pentru gestionarea bazei de date
- **SQL Server** - Baza de date
- **Bootstrap & CSS** - Design responsive
- **JavaScript (jQuery)** - Interactivitate pe frontend



