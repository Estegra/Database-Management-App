
-- ThesisID 1, UniversityID 1, AuthorID 1, SupervisorID 2
INSERT INTO test.university (UniversityName) 			VALUES("Sakarya Üniversitesi");
INSERT INTO test.institute (InstituteName,UniversityID) VALUES("Fen Bilimleri Enstitüsü",1);
INSERT INTO test.person (PersonName,PersonTitle) 		VALUES("Günay Karlı",NULL);
INSERT INTO test.person (PersonName,PersonTitle) 		VALUES("Ayhan Özdemir","DOÇ. DR.");
INSERT INTO test.topic (TopicName) 						VALUES("Bilgisayar Mühendisliği Bilimleri");
INSERT INTO test.topic (TopicName) 						VALUES("Bilgisayar ve Kontrol");

INSERT INTO Thesis (Title, Abstract, AuthorID, Year, Type, SupervisorID, UniversityID, InstituteID, NumberOfPages, Language, SubmissionDate)
		VALUES ("Bilgisayar öğrenmesinde yeni tümevarımsal algoritmalar", "Bilgisayar teknolojisinin ilerlemesine paralel olarak, veri toplama kaynakları da oldukça gelişmiş ve yaygınlaşmıştır.
        Hemen her disiplin, kendi ürettiği veriler ile bereber dış çevreden elde ettiği verilerle, çeşitli amaçlar için büyük boyutlarda veritabanları oluşturmuştur. Bu verilerin içerisinde çok
        değerli ve gelecek için karar vermeyi sağlayacak bilgiler mevcuttur. Günümüzde bu veritabanlarından anlamlı ve yararlı bilgiyi ortaya çıkarabilmek önemli bir araştırma alanı haline 
        gelmiştir.Uzman sistemlerin en önemli görevlerinden biri, veritabanları içerisindeki yararlı, fakat saklı bilginin ortaya çıkartılmasıdır. Bilginin elde edilmesi, bilgi kazanımı olarak
        da isimlendirilir. Uzman sistemlerde, bilginin gösterilmesi, kullanılması ve kazanılması ile ilgili çözülmesi gereken önemli problemler bulunmaktadır. Bunlar içerisinde bilgi kazanımı 
        problemi en kritik aşamayı oluşturmaktadır. Bu çalışmada, bilgiyi elde etmek amacı ile geliştirilen makine öğrenmesi algoritmaları (IREM ve keREM) sunulacaktır."
	
        , 1, 2010, "Doctorate", 2, 1, 1, 103, "Turkish", "2010-08-02");
        
INSERT INTO test.thesistopic (ThesisNo,TopicID) 		VALUES (1, 1);
INSERT INTO test.thesistopic (ThesisNo,TopicID) 		VALUES (1, 2);
UPDATE test.thesis 
SET Keywords = "Algoritma,Bilgisayar Öğrenmesi,Machine Learning,Veri"
WHERE thesis.ThesisNo = 1;
        
        
-- ThesisID 2, UniversityID 2, AuthorID 1, SupervisorID 3

INSERT INTO test.university (UniversityName)			VALUES("Fatih Üniversitesi");
INSERT INTO test.institute (InstituteName,UniversityID)	VALUES("Fen Bilimleri Enstitüsü",2);
INSERT INTO test.person (PersonName,PersonTitle) 		VALUES("M. Sabih Aksoy", "DOÇ. DR.");
INSERT INTO test.topic 	(TopicName)						VALUES ("Elektrik ve Elektronik Mühendisliği");

INSERT INTO Thesis (Title, Abstract, AuthorID, Year, Type, SupervisorID, UniversityID, InstituteID, NumberOfPages, Language, SubmissionDate)
		VALUES ("Application of rule indiction algorithms to DNA sequence analysis", " In this study, Rules-3, which is one of the effective rule induction algorithm, was used to analys to 
        DNA sequence. In the experiments, different option of Rules-3 were used, and according to the amprical comparisons, the algorithm appeared to be comparable to well- known algorithms 
        in terms of the accuracy of the extracted rule in classifying unseen data. A detail literature survey on application of inductive learning techniques to DNA sequence analysis was also 
        conducted and review of the related work from the literature was prapered."
        , 1, 2000, "Master", 3, 2, 1, 43, "English", "2000-08-17");
        
INSERT INTO test.thesistopic (ThesisNo,TopicID) 		VALUES (2, 3);
UPDATE test.thesis 
SET Keywords = "DNA,Algorithm,Sequence,Experiment"
WHERE thesis.ThesisNo = 2;


-- Thesis ID 3, UniversityID 3,AuthorID 4, SupervisorID 5

INSERT INTO test.university (UniversityName) 			VALUES("Yeditepe Üniversitesi");
INSERT INTO test.institute (InstituteName,UniversityID) VALUES("Sosyal Bilimler Enstitüsü",3);
INSERT INTO test.person (PersonName,PersonTitle) 		VALUES("Hatice Karadoğan",NULL);
INSERT INTO test.person (PersonName,PersonTitle) 		VALUES("Teymur Rzayev","PROF. DR.");
INSERT INTO test.topic (TopicName) 						VALUES("Güzel Sanatlar");

INSERT INTO Thesis (Title, Abstract, AuthorID, Year, Type, SupervisorID, UniversityID, InstituteID, NumberOfPages, Language, SubmissionDate)
		VALUES ("The history of the blackboard and artists using the blackboard in art and their works", "In this study, it is aimed to investigate the 'History of Blackboard and The Artists 
        Using Blackboard in Art and Their Works' which is a subject that has not been addressed sufficiently in our country and in the world literature. Questions such as 'Where and when was 
        it used for the first time? In what period, for what purpose was it used? etc. shed light on the subject. As a result of this research, it has been resolved that the existence of 
        Blackboard in education is related to writing. It has been observed that the tablets, which started in the Sumerian era and were used as student notebooks for a long time, were used 
        in the form of stone, wood and wax tablets in the subsequent civilizations. The Sumerian student clay tablet, the Etruscan tablet used around 700 BC and the Greek wax tablet are the 
        ancestors of the blackboards used today. In literature studies, it was seen that the use of large blackboards in classrooms was in 1801 for the first time and after 1841 it was more 
        widely distributed and used gradually in central schools, then in town and village schools. In the second half of the 20th century, it was replaced by colored and technological boards.
        In this study, in which the historical process of the blackboard is examined comprehensively, its relationship with art and (while it is used less in education in our century) and why 
        it affected the artist so much, are focused on. It aims to investigate why and by whom the blackboard form, which was first witnessed in the works of Joseph Beuys, was used. 
        Considering the period and the years it was made and referring to the reasons for choosing this surface its presence in art in various mediums has been demonstrated with case studies
        by addressing from educational, historical, cultural, nostalgic, identical, ethnic etc. contexts."
	
        , 4, 2020, "Proficiency in Art", 5, 3, 1, 236, "English", "2020-07-08");

INSERT INTO test.thesistopic (ThesisNo,TopicID) 		VALUES (3, 4);   
UPDATE test.thesis 
SET Keywords = "Blackboard,Text,Subject,Media,Context"
WHERE thesis.ThesisNo = 3;


-- Thesis ID 4, UniversityID 4,AuthorID 6, SupervisorID 7

INSERT INTO test.university (UniversityName) 			VALUES("Marmara Üniversitesi");
INSERT INTO test.institute (InstituteName,UniversityID) VALUES("Türkiyat Araştırmaları Enstitüsü",4);
INSERT INTO test.person (PersonName,PersonTitle) 		VALUES("Hüsnü Ada",NULL);
INSERT INTO test.person (PersonName,PersonTitle) 		VALUES("Ali Akyıldız","PROF. DR.");
INSERT INTO test.topic (TopicName) 						VALUES("Tarih");

INSERT INTO Thesis (Title, Abstract, AuthorID, Year, Type, SupervisorID, UniversityID, InstituteID, NumberOfPages, Language, SubmissionDate)
		VALUES ("Osmanlı Devletinin hizmetinde ilk modern Osmanlı sivil toplum örgütü: Osmanlı Hilâl-i Ahmer Cemiyeti (1868-1911)", "22 Ağustos 1864 tarihli Cenevre Sözleşmesi gereğince 
        savaşlarda yaralanan ve hastalanan askerlere yardım etmek üzere kurulan bağımsız milli yardım cemiyetlerinin Osmanlı şubesi ancak 14 Nisan 1877'de Osmanlı-Rus Harbi (1877-1878) 
        arefesinde kurulabilmişti. Savaş boyunca 28 hastane kurmak suretiyle 47 bin yaralı ve hasta Osmanlı askerini tedavi eden Cemiyet, savaş ertesinde II. Abdülhamid'ce pragmatik bir 
        şekilde sadece savaş dönemlerinde (1897 Osmanlı- Yunan Harbi ve 1904-5 Osmanlı- Rus Harbi) ya da kolera salgınlarında kullanılmak istenmiş bunun dışında sadece kağıt üzerinde 
        faaliyet göstermesine izin verilmiştir. Diğer taraftan hilâl-i ahmer alametinin kızılhaç ile aynı statüde sayılması hukuken ancak 1 Kasım 1911'de gerçekleştirilebilmiştir. Günümüz 
        Türk Kızılay Derneği'ne dönüşecek olan Osmanlı Hilâl-i Ahmer Cemiyeti özellikle savaş zamanlarında ortaya çıkan devletin hizmetindeki bir sivil toplum örgütüydü. Bu çalışmada 
        devlet kurumu kimliğinden yarı-resmi modern bir sivil toplum örgütüne dönüşüm gözler önüne serilmektedir."
	
        ,6, 2011, "Doctorate", 7, 4, 1, 351, "Turkish", "2011-03-14");

INSERT INTO test.thesistopic (ThesisNo,TopicID) 		VALUES (4, 5);   
UPDATE test.thesis 
SET Keywords = "Osmanlı,Tarih,Tıp,Sivil Toplum Örgütleri,Yakın Çağ"
WHERE thesis.ThesisNo = 4;


-- Thesis ID 5, UniversityID 5,AuthorID 8, SupervisorID 9

INSERT INTO test.university (UniversityName) 			VALUES("İstanbul Üniversitesi");
INSERT INTO test.institute (InstituteName,UniversityID) VALUES("Sosyal Bilimler Enstitüsü",5);
INSERT INTO test.person (PersonName,PersonTitle) 		VALUES("Talha Kaan Ünlü",NULL);
INSERT INTO test.person (PersonName,PersonTitle) 		VALUES("Namık Sinan Turan","PROF. DR.");
INSERT INTO test.topic (TopicName) 						VALUES("Sosyoloji");

INSERT INTO Thesis (Title, Abstract, AuthorID, Year, Type, SupervisorID, UniversityID, InstituteID, NumberOfPages, Language, SubmissionDate)
		VALUES ("Portekiz'den Osmanlı topraklarına Yahudi göçü: Osmanlı İmparatorluğu'nda Sefaradların sosyo-kültürel tarihi", "Antik çağlardan beri sürgün edilerek göçe mecbur bırakılan 
        Yahudiler dünyanın dört bir köşesine yayılmış, çok geniş bir coğrafyada varlık göstererek diaspora oluşturmuşlardır. İber Yarımadası da Yahudilerin yerleşim kurdukları ve yüzyıllar 
        boyunca hayatlarını sürdürdükleri yerlerden biridir. Bu bölgede Romalılar, Vizigotlar ve Müslümanlar gibi farklı güçlerin yönetimi altında yaşamış, 1139 yılında Portekiz Krallığı'nın 
        kurulmasıyla yeni krallığa dahil olmuşlardır. Portekiz'de özellikle ilk hanedan döneminde iyi şartlarda yaşamış ve krallığın önemli mevkilerinde yer almış olsalar da 1496 yılında 
        çıkarılan fermanla geçmişteki travmaları tekrarlanmış, Portekiz'i terk etmek zorunda kalmışlardır. İber Yarımadası'ndan ayrılmalarının ardından özellikle Akdeniz havzasındaki 
        topraklara göç eden Yahudilerin en önemli duraklarından biri Osmanlı İmparatorluğu olmuştur. Sultan II. Bayezid tarafından kabul edilen Sefaradlar Osmanlı topraklarında çeşitli
        bölgelere yerleşmiş, imparatorluğun demografik yapısını etkilemişlerdir. Osmanlı toplumuna kısa sürede uyum sağlayarak İber Yarımadası'ndan getirdikleri bilgi, birikim ve 
        tecrübeleriyle imparatorluğa katkı sunmuş, toplumun bir parçası haline gelmişlerdir. Sefarad Yahudilerinin İber Yarımadası'ndan Osmanlı topraklarına göçünü konu alan bu çalışma batı
        kaynakları, Yahudi literatürü ve Osmanlı arşiv belgelerini bir araya getirerek farklı bir perspektif sunmayı amaçlamaktadır."
	
        ,8, 2023, "Master", 9, 5, 1, 187, "Turkish", "1970-01-01");

INSERT INTO test.thesistopic (ThesisNo,TopicID) 		VALUES (5, 5);
INSERT INTO test.thesistopic (ThesisNo,TopicID) 		VALUES (5, 6);   
UPDATE test.thesis 
SET Keywords = "Tarih,Portekiz,Yahudi,Osmanlı"
WHERE thesis.ThesisNo = 5;

 
 -- Thesis ID 6, UniversityID 4,AuthorID 10, SupervisorID 11 

INSERT INTO test.person (PersonName,PersonTitle) 		VALUES("Arif Ferda Kermelioğlu",NULL);
INSERT INTO test.person (PersonName,PersonTitle) 		VALUES("Aydın Uğur","PROF. DR.");
INSERT INTO test.topic (TopicName) 						VALUES("Devlet");

INSERT INTO Thesis (Title, Abstract, AuthorID, Year, Type, SupervisorID, UniversityID, InstituteID, NumberOfPages, Language, SubmissionDate)
		VALUES ("La Societe civile dans le processus de democratisation", "RESUME L'expansion de la democratic dans la plupart des spheres privees nous permet d'evaluer la societe 
        civile comme le modele social adequat applicable dans l'ordre democratique. Le but de cet essai est de presenter une theorie de la societe civile, comme un ideal utopique 
        de la democratic moderne structuree sur l'idee d'un modele d'institutionnalisation et d'orientation du conflit, plutöt qu'un modele du contrat social; une nouvelle definition 
        de la societe civile basee sur les perspectives d'une interpretation multidimensionnelle de la totalite d'interactions sociales; une condition de la reproduction de la 
        democratic, plutot qu'une alternative aux institutions democratiques de l'Etat. L'evolution historique des aspects essentiels, les conceptualisations classiques de la societe 
        civile et de la democratic, leur autonomie theorique face â l'Etat, leurs dimensions institutionnelles, et leurs interdependences au niveau du discours social sont abordes en 
        profondeur: les egalites et liberies politiques et sociales, la pluralite sociale, les fonctions integrateurs des spheres autonomes de Taction sociale, l'utilisation de la 'publicite'
        comme un moyen de communication social. La possibility de conceptualiser et d'institutionnaliser la societe civile comme le proces normatif davantage de democratization, et les 
        formulations theoriques pour appuyer le cadre de 1'interdependance des concepts forment l'objectif general de l'etude. L'interpretation moderne de la societe civile est reconstruite
        comme une formulation forte, democratique, et progressive de la pluralite sociale; un consensus normatif basesur des principes ethiques et moraux de l'autonomie individuelle et de 
        la subjectivite. Le but de ce travail est de fournir une analyse nonnative et structurale, qui sera admise valide pour la plupart des interactions generates entre la societe civile
        et la democratie et sera base sur une analyse theorique et conceptuelle liee â ces concepts, au lieu de faire une etude de cas pratique et factuelle."
	
        ,10, 1999, "Master", 11, 4, 1, 95, "French", "1970-01-01");

INSERT INTO test.thesistopic (ThesisNo,TopicID) 		VALUES (6, 6);
UPDATE test.thesis 
SET Keywords = "Democracy,State,Societe"
WHERE thesis.ThesisNo = 6;
        