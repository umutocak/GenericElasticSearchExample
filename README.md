# ElasticsearchManager Açıklama ve Kullanım Kılavuzu

Bu dokümantasyon, `ElasticsearchManager` sınıfını ve içerisinde bulunan metodları açıklamaktadır. `ElasticsearchManager` sınıfı, ElasticSearch ile yapılan işlemleri daha kolay ve esnek bir şekilde yönetebilmek için geliştirilmiştir. Bu sınıf, CRUD işlemleri, arama sorguları, ve index işlemleri gibi bir dizi ElasticSearch özelliğini sağlar.

## İçindekiler

- [Proje Tanımı](#proje-tanımı)
- [Kullanılan Teknolojiler](#kullanılan-teknolojiler)
- [Metodlar](#metodlar)
  - [BoolQueryAsync](#boolqueryasync)
  - [CountDocumentsAsync](#countdocumentsasync)
  - [CreateDocumentAsync](#createdocumentasync)
  - [CreateIndexAsync](#createindexasync)
  - [DeleteDocumentAsync](#deletedocumentasync)
  - [ExistsQueryAsync](#existsqueryasync)
  - [FuzzyQueryAsync](#fuzzyqueryasync)
  - [GetDocumentAsync](#getdocumentasync)
  - [GetDocumentsAsync](#getdocumentsasync)
  - [MatchQueryAsync](#matchqueryasync)
  - [SearchAsync](#searchasync)
  - [TermQueryAsync](#termqueryasync)
  - [UpdateDocumentAsync](#updatedocumentasync)
  - [WildcardQueryAsync](#wildcardqueryasync)
  
## Proje Tanımı

Bu proje, Elasticsearch ile daha kolay ve esnek bir şekilde işlem yapabilmek için geliştirilmiş bir yapıyı sunmaktadır. `ElasticsearchManager` sınıfı, ElasticSearch üzerinde çeşitli işlemler gerçekleştirebilmek için bir dizi metod sunar. Bu metodlar arasında belge ekleme, arama, güncelleme ve silme işlemleri bulunur. Ayrıca, çeşitli sorgulama metodları ile Elasticsearch üzerinde daha sofistike aramalar yapılabilir.

## Kullanılan Teknolojiler

- **Elasticsearch**: Veri depolama ve sorgulama aracı.
- **.NET Core**: Uygulama geliştirme çerçevesi.
- **C#**: Uygulama dili.
- **Expression<Func<T, object>>**: Lambda ifadeleri ile dinamik sorgular oluşturma.

## Metodlar

### BoolQueryAsync

Bu metod, birden fazla sorguyu bir araya getirip mantıksal ilişki ile birleştirir. Bu sayede karmaşık sorgular oluşturulabilir. 

**Parametreler**:
- `matchField`: Eşleşen alan.
- `matchQueryKeyword`: Eşleşen kelime.
- `fuzzyField`: Fuzzy arama yapılacak alan.
- `fuzzyQueryKeyword`: Fuzzy arama kelimesi.
- `wildcardField`: Wildcard arama yapılacak alan.
- `wildcardQueryKeyword`: Wildcard arama kelimesi.
- `indexName`: İndeks adı (varsayılan "products").
- `from`: Sorgu başlangıcı.
- `size`: Sorgu sonucu sayısı.

### CountDocumentsAsync

Belirtilen indeksin içindeki toplam belge sayısını döndürür.

**Parametreler**:
- `indexName`: İndeks adı (varsayılan "products").

### CreateDocumentAsync

Yeni bir belge oluşturur ve belirtilen indeste saklar.

**Parametreler**:
- `document`: Oluşturulacak belge.
- `indexName`: İndeks adı (varsayılan "products").

### CreateIndexAsync

Yeni bir indeks oluşturur.

**Parametreler**:
- `indexName`: İndeks adı (varsayılan "products").

### DeleteDocumentAsync

Belirtilen id'ye sahip belgeyi siler.

**Parametreler**:
- `documentId`: Silinecek belgenin id'si.
- `indexName`: İndeks adı (varsayılan "products").

### ExistsQueryAsync

Bir alanın var olup olmadığını kontrol eder.

**Parametreler**:
- `field`: Kontrol edilecek alan.
- `indexName`: İndeks adı (varsayılan "products").

### FuzzyQueryAsync

Fuzzy (benzer) arama yapar, özellikle yanlış yazılmış kelimeleri bulmak için kullanılır.

**Parametreler**:
- `field`: Arama yapılacak alan.
- `queryKeyword`: Arama kelimesi.
- `indexName`: İndeks adı (varsayılan "products").

### GetDocumentAsync

Belirli bir id'ye sahip belgeyi getirir.

**Parametreler**:
- `documentId`: Getirilecek belgenin id'si.
- `indexName`: İndeks adı (varsayılan "products").

### GetDocumentsAsync

Belirtilen indeksteki tüm belgeleri getirir.

**Parametreler**:
- `indexName`: İndeks adı (varsayılan "products").

### MatchQueryAsync

Bir alan üzerinde metin analizi yaparak eşleşen verileri getirir.

**Parametreler**:
- `field`: Arama yapılacak alan.
- `queryKeyword`: Arama kelimesi.
- `indexName`: İndeks adı (varsayılan "products").

### SearchAsync

Daha genel bir sorgulama metodudur. Arama sorgusu için daha özelleşmiş bir yapı kullanılır.

**Parametreler**:
- `searchRequestDescriptor`: Arama talimatları.

### TermQueryAsync

Bir alanda tam eşleşme araması yapar.

**Parametreler**:
- `field`: Arama yapılacak alan.
- `queryKeyword`: Arama kelimesi.
- `indexName`: İndeks adı (varsayılan "products").

### UpdateDocumentAsync

Belirli bir id'ye sahip belgeyi günceller.

**Parametreler**:
- `documentId`: Güncellenecek belgenin id'si.
- `partialDocument`: Güncellenecek veri.
- `indexName`: İndeks adı (varsayılan "products").

### WildcardQueryAsync

Wildcard araması yapar, örneğin `*test*` şeklinde. Veri sorgulama sürecinde belirli bir kalıba uygun taramada bulunabilmek için yıldız(*) ve soru işareti(?) gibi joker karakterlerden istifade etmekteyiz.
?s* => ikinci harfi 's' ile başlayanları getirir

**Parametreler**:
- `field`: Arama yapılacak alan.
- `queryKeyword`: Arama kelimesi.
- `indexName`: İndeks adı (varsayılan "products").


---

Bu sınıf, Elasticsearch ile yapılan işlemleri basitleştirmek için birçok yararlı metod sunmaktadır.
