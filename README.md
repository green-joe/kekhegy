# WPF-MySQL feladat
## Kékhegy Szálló

###Készíts adatbázist, illetve WPF alkalmazást, amelyben a következő feladataid vannak:

Hozz lére egy kekhegy nevű adatbázist a MySQL szerveren, amely UTF-8 kódolású magyar ékezetes karakterekkel!
Az adatbazis.sql-ből importáld be a táblákat és az adatokat a kekhegy adatbázisba!
Hozz lére WPF alkalmazást, amelyben az ablakon belül fülek vannak (TabControl), amelyből 2 darab van! TabControl helyett lehet más megoldást is használni, pl. menü, vagy külön ablakban megjelenítés.
Az első fülön legyen lehetőség arra, hogy a szobák adatait lehessen megjeleníteni DataGrid-ben! A Datagrid-ben egyszerre csak egy sort lehessen kijelölni, és a tartalmát ne lehessen módosítani! Az adatokat a szobak nevű táblából kérdezd le!
Ugyanezen a fülön legyen lehetőség arra, hogy a kijelölt sor törölhető legyen a táblából! A törlés előtt hozz létre Trigger-t a szobak táblában, amely törlés előtt az összes foglalást törli a foglalasok táblában! (Lásd órai példa) Előtte jelenjen meg egy előugró ablakban, hogy biztos vagy-a törlésben!

A 2. fülön lehessen szobát foglalni a következőképpen:
A fül betöltésekor töltődjön be az összes szobához rendelt azonosítószám egy ComboBox-ba. Az azonosítószám alatt itt nem az id értendő, hanem úgy kaphatod meg, hogy az emelet után közvetlenül hozzáfűzöd a szoba számát! Az összefűzéshez használhatod a CONCAT függvényt! Pl. 01,02,…,1101,1102, 2201,2202 szobák.
Legyen 3 beviteli mező, amelybe be lehet írni a nevet, dátumot és a napok számát!
Legyen egy gomb létrehozva, amelyre kattintva belekerül a foglalasok táblába a megfelelő adat. Figyelj arra, hogy ehhez szükséges van a vendég és a szoba azonosítószámára! A vendég azonosítószámát lekérdezéssel meg tudod határozni (feltesszük, hogy minden vendég neve egyedi), a szoba azonosítószámát meg érdemes eltárolni pl. egy listába, amikor a ComboBox-ba bekerül az adat.
A foglalás azonosítója legyen az utolsó foglalás száma+1 érték!
A gombra kattintás után, mielőtt az adat belekerülne az adatbázisba, ellenőrizd, hogy ki van-e választva szoba a ComboBox-ból, a név szerepel-e a vendegek táblában, illetve a dátum és a foglalt napok száma ki van listázva!
Bevitel után jelenjen meg előugró ablakban, hogy „A foglalás sikeres.”!
