# Magic Legend GAME DESIGN DOCUMENT (Oyun Tasarım Belgesi)
## 1. Oyun İsmi
 - Oyun Adı: Magic Legend
 - Genre: HybridCasual - Runner
 - Art Style: 3D - Isometric Perspective

## 2. Oyun Tanımı
"Magic Legend" -Oyuncular, karakterlerini hareket ettirirken element büyülerini kullanma yeteneğine sahiptir. Oyun, oyuncuların hayatta kalmak ve elementlerini geliştirmek için sonlu koşu sırasında düşmanlarla savaştığı bir mobil oyun türüdür.

### 2.1. Oyun Başlangıcı
Oyuncu, oyun başladığında küçük bir alanda (başlama yeri) bulunur. Bu alanda bir Market bulunur. Başlama yerinden Market'e giderse, kendi element büyülerini geliştirebileceği bir alana ulaşır. Market, element yükseltme ve karakter geliştirme için kullanılan temel bir mekandır.

Başlama yerindeki koşuya başlama yerine dönerse, sonlu bir koşuya başlar. Oyuncu, sonlu bir koşu alanında ileri doğru ilerler ve karşısına rastgele yerleştirilmiş düşmanlar, toplanabilir materyaller, hazineler ve engeller çıkar. Oyuncu, bu koşu sırasında element büyülerini kullanabilir ve elementlerini geliştirmeye çalışır.

### 2.2. Element Büyüleri
Oyuncu, element büyülerini kullanabilir. Her bir elementin farklı büyüleri ve seviyeleri vardır (örnek olarak 3. 5. 10. seviyeler). Oyuncu, elementlerini geliştirmek ve yeni yetenekler kazanmak için oyun içi "Upgrade Kit" adı verilen özel nesneleri toplamalıdır. "Upgrade Kit", Düşman öldürdükçe düşük ihtimalle kazanılabilir ya da marketten satın alınabilir. Elementlerin seviyeleri yükseldikçe, büyüler daha güçlü hale gelir ve oyuncu daha fazla yetenek kazanır.

### 2.3. Sonlu Koşu ve Bölümler
Oyun, sonlu bir koşu sırasında ilerler. Oyuncu, düşmanları alt etmeli, kaynakları toplamalı ve elementlerini geliştirmelidir. Düşmanlar ve materyaller rastgele yerleştirilir.

Bölümün sonuna doğru, nadiren "BOSS AREA" portalı çıkacaktır. Oyuncu bu porta girerse, büyük ödüllere sahip bir boss ile savaşabilir. Bu bölümde, kontroller isometric bir görünüme geçer ve karakter, Joystick ile 4 farklı yönde hareket edebilir.
Bölümler arası geçiş 2 türlüdür. Oyunun sonuna ulaşıp bir sonraki bölüme geçilebilir ya da koşu alanında rastgele beliren portallara girerek Event Level'larına erişim sağlayabilir. Event Level'lar, oyuncunun bolca materyal toplayabileceği bir bölüm olup kendini geliştirmesine imkan tanır.

## 3. Oyun Mekaniği
**Karakter Kontrolleri:** Oyuncu, karakterini sağa ve sola yönlendirebilir ve başlangıçta sadece temel vuruş yeteneğine sahiptir. Karakter, bu sonlu koşu sırasında ileri doğru hareket eder. Eğer Boss Area' ya girerse 4 yöne joystick yardımıyla hareket edebilecektir.

**Element Büyüleri:** Oyuncu, element büyülerini kullanabilir. Bu büyülerin farklı seviyeleri vardır (örnek olarak 3. 5. 10. seviyeler). Her seviyede yeni yetenekler kullanabilir. Oyuncu, elementlerinin seviyelerini yükseltmek için "Upgrade Kit" adı verilen özel nesneleri bulmalıdır. Yeni yetenekler oyuncuyu daha güçlü yapar.
  #### Elementler
    -Buz
    -Ateş
    -Hiçlik (Void)
**Oyun Dünyası:** Oyun, sonlu bir koşu alanında geçer. Bu alan boyunca düşmanlar, toplanabilir materyaller, portallar ve hazine sandıkları rastgele yerleştirilir. Portallar benzer farklı bölümlere geçiş sağlar.

**Combo Streak:** Oyuncu, ardışık saldırılar yaparak daha fazla hasar verebilir (Combo Streak). Bu, oyuncunun oyun içi para kazanmasına yardımcı olabilir.

**Boss Alanı:** Bölümün sonunda nadiren "BOSS AREA" portalı belirir. Bu portala giren oyuncu, büyük ödüllere sahip bir boss ile savaşır. Bu bölümde kontroller isometric görünüme geçer ve karakter, Joystick ile 4 farklı yönde hareket edebilir.

## 4. Karakterler ve Dünya
**Ana Karakter:** Oyuncu, kendi karakterini kontrol eder. Karakter, element büyülerini kullanabilir ve oyun ilerledikçe daha fazla yetenek kazanır. Karakter görünümü, Bedenine göre büyük bir kafa ve normal bir insan vücuduna benzer ve sanat stili olarak orta-yüz (Mid Poly) şeklinde olmaktadır.

**Düşmanlar:** Oyunda farklı düşmanlar (kuklalar, canavarlar vb.) bulunur. Düşmanlar, karakterin ilerlemesini engellemek için sonlu koşu alanında rastgele yerleştirilir.
  #### Düşmanlar
    -BOSS
    -Atış Kuklası
    -Agresif Canavar
## 5. Hikaye ve Görevler
Oyunda ana bir hikaye örgüsü bulunmaz. Oyuncunun amacı, sonlu koşu sırasında düşmanları yenmek, kaynakları toplamak ve elementlerini geliştirmektir.

## 6. Görsel ve Sanat Stil
Görsel tasarım, oyunda kullanılan 3D karakterlerin orta seviyede (Mid-Poly) olacağını içerir. Ayrıca, 2D görseller çizgi film tarzında tasarlanacaktır.

## 7. Ses ve Müzik
Oyunda kullanılan ses efektleri ve müzikler, ücretsiz kaynaklardan veya ses programlarından temin edilecektir.

## 8. Teknik Gereksinimler
Oyun, mobil platformlar için geliştirilecektir ve Unity oyun motoru kullanılacaktır.

# Game Visuals Examples
## Character Pack
![CharacterPack](https://github.com/smhucr/MagicLegend/blob/main/Visuals/DKO_CharPack.png)

## Game Art Style
![SwingingSpire](https://github.com/smhucr/MagicLegend/blob/main/Visuals/Swingin%20Spire%20Example.png)

![RobloxEx](https://github.com/smhucr/MagicLegend/blob/main/Visuals/M%C4%B0d%20poly%20Example.png)

