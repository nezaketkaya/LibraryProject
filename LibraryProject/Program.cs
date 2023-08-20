using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace LibraryProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /* Yerli ve yabancı, 2 kategoriden oluşan kitap listesi. 
               Öğrenci kitap listesini görebilecek, yazarlarla ilgili bilgi alabilecek.
               Öğrenci kitap satın alabilecek.
               Kendi bilgilerini metin dosyasına kaydedebilecek. */


            Console.WriteLine("ID. Türkçe Kitaplar                   ID. Yabancı Kitaplar   \n");

            string[,] tablo = new string[7, 2];

            tablo[0, 0] = "1. Reşat Nuri - Çalıkuşu";
            tablo[0, 1] = "2. Jules Verne - Tuna Kılavuzu";
            tablo[1, 0] = "3. Yakup Kadri - Yaban";
            tablo[1, 1] = "4. Jack London - Martı";
            tablo[2, 0] = "5. Oğuz Atay - Tutunamayanlar";
            tablo[2, 1] = "6. Dostoyevski - Kumarbaz";
            tablo[3, 0] = "7. Halide Edip - Sinekli Bakkal";
            tablo[3, 1] = "8. Anton Çehov - Altıncı Koğuş";

            KitapUcret[] kitaplar = new KitapUcret[]
       {
            new KitapUcret { KitapID = 1, KitapUcreti = 35 },
            new KitapUcret { KitapID = 2, KitapUcreti = 25 },
            new KitapUcret { KitapID = 3, KitapUcreti = 28 },
            new KitapUcret { KitapID = 4, KitapUcreti = 47 },
            new KitapUcret { KitapID = 5, KitapUcreti = 70 },
            new KitapUcret { KitapID = 6, KitapUcreti = 22 },
            new KitapUcret { KitapID = 7, KitapUcreti = 52 },
            new KitapUcret { KitapID = 8, KitapUcreti = 33 }
       };

            for (int i = 0; i < 4; i++)
            {
                Console.Write(tablo[i, 0]);
            
                    int length = tablo[i, 0].Length;
               
               while (length < 35) 
                  {
                      Console.Write(" ");
                      length++;
                  } 
                  Console.WriteLine(tablo[i,1]);
            }
            
            Console.WriteLine();
            Console.WriteLine("***************************************************");
            Console.WriteLine();

            Console.WriteLine("İşlemler Menüsü\n\n1. Fiyat sorgulama\n2. Yeni okur kaydı\n" +
                "3. Günün kitabı\n4. Kitap arşivi\n5. Kitap satın al\n");
          
            Console.WriteLine("İşlem seçin: ");
            int islem = Convert.ToInt16(Console.ReadLine());

            switch (islem)
            {
                case 1:

                    Console.WriteLine("Fiyatını öğrenmek istediğiiniz kitap id'sini yazın: ");
                    int arananid = Convert.ToInt16(Console.ReadLine());
                    int bulunanUcret;
                    foreach(var kitap in kitaplar)
                    {
                        if (kitap.KitapID == arananid)
                        {
                            bulunanUcret = kitap.KitapUcreti;                        }
                    }

                    break;
               
                case 2:

                    Console.WriteLine("Müşeteri bilgilerini giriniz: ");

                    MusteriKaydi musteri1 = new MusteriKaydi();
                    Console.WriteLine("ID giriniz: ");
                    musteri1.ID = Convert.ToInt16(Console.ReadLine());
                    Console.WriteLine("İsim giriniz: ");
                    musteri1.isim = Console.ReadLine();
                    Console.WriteLine("Soyisim giriniz: ");
                    musteri1.soyisim = Console.ReadLine();
                    Console.WriteLine("Kitap ismini giriniz: ");
                    musteri1.kitap = Console.ReadLine();

                    string dosya = @"C:\Users\ali\Desktop\MusteriKaydi.txt";
                    StreamWriter sw = new StreamWriter(dosya);
                    
                    sw.WriteLine("ID: \n"+ musteri1.ID + "İsim: \n" + musteri1.isim +
                                 "Soyisim: \n" + musteri1.soyisim + "Kitap: \n"+musteri1.kitap);
                    sw.Close();
                    break;

                case 3:

                    Console.WriteLine("İşlem: Günün kitabı."); // Kitap listesinden rastgele kitap seçeceğiz.
                    
                    Random random = new Random();
                    int randomIndex = random.Next(0, 3);
                    int randomIndex2 = random.Next(0, 1);
                    string selectedBook = tablo[randomIndex,randomIndex2];
                    Console.WriteLine("Rastgele seçilen kitap: " + selectedBook);

                    break;

                 case 4:

                    Console.WriteLine("İşlem: Kitap arşivi \n");
                    FileStream fs = new FileStream(@"C:\Users\ali\Desktop\kitaparsivi.txt", FileMode.Open, FileAccess.Read);
                    StreamReader sr = new StreamReader(fs);
                    string metin = sr.ReadLine();
                    while (metin !=  null)
                    {
                        Console.WriteLine(metin);
                        metin = sr.ReadLine();
                    }
                    sr.Close();
                    fs.Close();

                    break;
                    
                case 5:

                    bool isTrue = true; 
                    int toplamTutar = 0;

                    while (isTrue) // 50 tane kitap alabilir.
                    {
                        Console.WriteLine("Satın alacağınız kitabın id'sini giriniz: ");
                       
                        int Tutar = 0;
                        int satinalid = Convert.ToInt32(Console.ReadLine());

                        //Find metodu ile girilen id'ye karşılık gelen kitap ücretibulunabilir.
                        KitapUcret satinal = Array.Find(kitaplar, kitap => kitap.KitapID == satinalid);

                        if (satinal != null) // Kitap bulunup bulunmadığını kontrol edin
                        {
                            Tutar = satinal.KitapUcreti;
                            Console.WriteLine("Kitap ücreti: "+Tutar);
                        }
                        else
                        {
                            Console.WriteLine("Bu ID'ye sahip bir kitap bulunamadı.");
                        }

                        Console.WriteLine("Başka kitap seçecekseniz 1'e, tutarı görmek için 2'ye basın: ");
                        int secim = Convert.ToInt32(Console.ReadLine());

                        if(secim == 1)
                        {
                          //isTrue zaten true olduğu için bir şey yazmaya gerek yok.
                        }
                        else if (secim == 2)
                        {
                            isTrue = false;
                        }
                        else
                        {
                            Console.WriteLine("Geçersiz işlem yaptınız.");
                        }
                        toplamTutar += Tutar;
                    }
                    Console.WriteLine("Tutar: " + toplamTutar);

                    break;


                default:
                    break;
            }

            Console.ReadLine();
        }
    }
}