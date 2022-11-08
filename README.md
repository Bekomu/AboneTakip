# AboneTakip
.NET 6.0, EntityFrameworkCore, CodeFirst, Generic Repository Pattern, MSSQL, AutoMapper, FluentValidation gibi teknolojilerin/geliştirme yöntemlerinin  kullanıldığı bir Katmanlı Mimari WEB API projesidir. Bu proje abonelerinizi takip edebileceğiniz,  tüketim veya önyükleme değerlerine göre fatura kesebileceğiniz bir Abone Takip uygulamasıdır. 


----




Projede Merkez Bankası EVDS (Elektronik Veri Dağıtım Sistemi) kullanılmıştır. (Bu sistem üyelik gerektirsen bir sistemdir! Bilgi için : https://evds2.tcmb.gov.tr/)
Abonelerin ödeme yaptığı para birimine göre fatura hesaplaması yapıldığından dolayı güncel kur bilgilerinin alınması gerekmektedir.
Merkez Bankası EVDS sisteminden alacağınız ApiKey'i proje içinde bulunan servisin EVDS_API_KEY const değerine girmeniz yeterlidir. 
EVDS'den gelen JSON datayı ayrıştırarak anlık olarak faturası kesilen abonenin kullandığı para birimine göre hesap yapılmaktadır. 

![image](https://user-images.githubusercontent.com/97511430/200560790-aa3c49c7-9772-4347-a9e8-62e49839b52f.png)


Uygulamayı çalıştırmak için sadece database connection stringini düzeltmeniz ve update-database komutunu çalıştırmanız yeterlidir. 
