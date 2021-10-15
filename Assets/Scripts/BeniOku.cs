
public class BeniOku
{
    /*
     * !! Prototip hazýrlarken inspectorden GameManager scriptinde yer alan isPrototyping aktif edilecek.. 
     * deneme aþamasýnda deaktif edilecek.. yayýnlama aþamasýnda kontrollerin kaldýrýldýðý bir sürüm yayýnlanýr.
     * 
     1) Tavalar veya topun üzerinde sekeceði nesneler hiyerarþi içerisindeki Obstacle objesinin childý
    olacak þekilde içerisine atýlmalý.. Bu nesnelere collider verilip colliderin material kýsmýna Bouncy
    fizik materyali atanmalý. Yine bu nesnelere rigid body, dragpan.cs , swipe.cs atýlmalý.
    DragPanin swipe alanýna eklediðimi swipe.cs sürüklenip býrakýlmalý. Drag speed 10 olmalý.. 
    tagleri pan olmalý. Tavalarýn isimleri ayný olup yukarsan aþaðýya sýrarýyla 1 2 3 þeklinde isim numaralandýrýlmalý..

    2) Leveller içerisinde bulunan dur objeleri topun durmasý gerektiði yere göre ayarlanacak. Topu 
    durduran bu nesnelerdeki collider olacak. Bu nesnelerin box collideri olmalý ve içerisine dur.cs 
    scripti atýlý olmalý. Tagi dur olmalý.
     
     3) topun oyun sonunda içine gireceði bardaðýn tagi final yapýlacak. adý kesinlikle cup olacak.
    Leveller içerisinde bulunan
    ayný bardak kullanýldýðý sürece sorun yok. Baþka bardak kullanýlacaksa animasyon ve effect 
    düzenlemek gerekebilir.

    4) topun yanlýþ yerlere gitmesinde oyunu bitirecek olan duvar nesneleri levele göre düzenlenmeli. 
    bu nesneler level prefablarýnýn içerisinde var buradan çoðaltýldýðý sürece sorun yok. Buradan
    çoðaltýlmassa ekli komponentler ve tagi düzenlenmeli.

    5) level controller içerisine playerin mükemmel baþarýlý atýþ için gerekli olan rotasyon deðeri
    vektör olarak girilecek. Her level için ayrý ayrý girilmeli.. Levelin düzenlemesi yapýlýrken 
    oyun baþlatýlýp önce player rotasyonu sonra teker teker tavalar düzenlenmeli her birinin transformu
    play anýnda kopyalanýp durdurulup yapýþtýrýlmasý.

     
     
     
     
     
     */
}
