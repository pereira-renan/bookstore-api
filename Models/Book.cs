using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    //public class IndustryIdentifiers
    //{
    //    [ForeignKey("VolumeInfo")]
    //    [JsonPropertyName("industryIdentifierId")]
    //    public Guid Id { get; set; }
    //    [JsonPropertyName("type")]
    //    public string? Type { get; set; }

    //    [JsonPropertyName("identifier")]
    //    public string? Identifier { get; set; }
    //    public virtual VolumeInfo? VolumeInfo { get; set; }
    //}

    public class ReadingModes
    {
        [ForeignKey("VolumeInfo")]
        [JsonPropertyName("readingModesId")]
        public Guid Id { get; set; }
        [JsonPropertyName("text")]
        public bool? Text { get; set; }

        [JsonPropertyName("image")]
        public bool? Image { get; set; }
        public virtual VolumeInfo? VolumeInfo { get; set; }
    }

    public class PanelizationSummary
    {
        [ForeignKey("VolumeInfo")]
        [JsonPropertyName("panelizationSummaryId")]
        public Guid Id { get; set; }
        [JsonPropertyName("containsEpubBubbles")]
        public bool? ContainsEpubBubbles { get; set; }

        [JsonPropertyName("containsImageBubbles")]
        public bool? ContainsImageBubbles { get; set; }
        public virtual VolumeInfo? VolumeInfo { get; set; }
    }

    public class ImageLinks
    {
        [ForeignKey("VolumeInfo")]
        [JsonPropertyName("imageLinksId")]
        public Guid Id { get; set; }
        [JsonPropertyName("smallThumbnail")]
        public string? SmallThumbnail { get; set; }

        [JsonPropertyName("thumbnail")]
        public string? Thumbnail { get; set; }
        public virtual VolumeInfo? VolumeInfo { get; set; }
    }

    public class VolumeInfo
    {
        [ForeignKey("BookData")]
        [JsonPropertyName("volumeInfoId")]
        public Guid Id { get; set; }
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("authors")]
        public List<string>? Authors { get; set; }

        [JsonPropertyName("publisher")]
        public string? Publisher { get; set; }

        [JsonPropertyName("publishedDate")]
        public string? PublishedDate { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        //[JsonPropertyName("industryIdentifiers")]
        //public virtual List<IndustryIdentifiers>? IndustryIdentifiers { get; set; }

        [JsonPropertyName("readingModes")]
        public virtual ReadingModes? ReadingModes { get; set; }

        [JsonPropertyName("printType")]
        public string? PrintType { get; set; }

        [JsonPropertyName("categories")]
        public List<string>? Categories { get; set; }

        [JsonPropertyName("averageRating")]
        public double? AverageRating { get; set; }

        [JsonPropertyName("ratingsCount")]
        public int? RatingsCount { get; set; }

        [JsonPropertyName("maturityRating")]
        public string? MaturityRating { get; set; }

        [JsonPropertyName("allowAnonLogging")]
        public bool? AllowAnonLogging { get; set; }

        [JsonPropertyName("contentVersion")]
        public string? ContentVersion { get; set; }

        [JsonPropertyName("panelizationSummary")]
        public virtual PanelizationSummary? PanelizationSummary { get; set; }

        [JsonPropertyName("imageLinks")]
        public virtual ImageLinks? ImageLinks { get; set; }

        [JsonPropertyName("language")]
        public string? Language { get; set; }

        [JsonPropertyName("previewLink")]
        public string? PreviewLink { get; set; }

        [JsonPropertyName("infoLink")]
        public string? InfoLink { get; set; }

        [JsonPropertyName("canonicalVolumeLink")]
        public string? CanonicalVolumeLink { get; set; }

        [JsonPropertyName("subtitle")]
        public string? Subtitle { get; set; }

        [JsonPropertyName("pageCount")]
        public int? PageCount { get; set; }

        public virtual BookData? BookData { get; set; }
    }

    public class ListPrice
    {
        [ForeignKey("SaleInfo")]
        [JsonPropertyName("listPriceId")]
        public Guid Id { get; set; }
        [JsonPropertyName("amount")]
        public double? Amount { get; set; }

        [JsonPropertyName("currencyCode")]
        public string? CurrencyCode { get; set; }

        [JsonPropertyName("amountInMicros")]
        public int? AmountInMicros { get; set; }
        public virtual SaleInfo? SaleInfo { get; set; }
    }

    public class RetailPrice
    {
        [ForeignKey("SaleInfo")]
        [JsonPropertyName("retailPriceId")]
        public Guid Id { get; set; }
        [JsonPropertyName("amount")]
        public double? Amount { get; set; }

        [JsonPropertyName("currencyCode")]
        public string? CurrencyCode { get; set; }

        [JsonPropertyName("amountInMicros")]
        public int? AmountInMicros { get; set; }
        public virtual SaleInfo? SaleInfo { get; set; }
    }

    public class SaleInfo
    {
        [ForeignKey("BookData")]
        [JsonPropertyName("saleInfoId")]
        public Guid Id { get; set; }
        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [JsonPropertyName("saleability")]
        public string? Saleability { get; set; }

        [JsonPropertyName("isEbook")]
        public bool? IsEbook { get; set; }

        [JsonPropertyName("listPrice")]
        public virtual ListPrice? ListPrice { get; set; }

        [JsonPropertyName("retailPrice")]
        public virtual RetailPrice? RetailPrice { get; set; }

        [JsonPropertyName("buyLink")]
        public string? BuyLink { get; set; }
        public virtual BookData? BookData { get; set; }

    }

    public class Epub
    {
        [ForeignKey("AccessInfo")]
        [JsonPropertyName("ePubId")]
        public Guid Id { get; set; }
        [JsonPropertyName("isAvailable")]
        public bool? IsAvailable { get; set; }

        [JsonPropertyName("acsTokenLink")]
        public string? AcsTokenLink { get; set; }
        public virtual AccessInfo? AccessInfo { get; set; }
    }

    public class Pdf
    {
        [ForeignKey("AccessInfo")]
        [JsonPropertyName("pdfId")]
        public Guid Id { get; set; }
        [JsonPropertyName("isAvailable")]
        public bool? IsAvailable { get; set; }

        [JsonPropertyName("acsTokenLink")]
        public string? AcsTokenLink { get; set; }
        public virtual AccessInfo? AccessInfo { get; set; }
    }

    public class AccessInfo
    {
        [ForeignKey("BookData")]
        [JsonPropertyName("accessInfoId")]
        public Guid Id { get; set; }

        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [JsonPropertyName("viewability")]
        public string? Viewability { get; set; }

        [JsonPropertyName("embeddable")]
        public bool? Embeddable { get; set; }

        [JsonPropertyName("publicDomain")]
        public bool? PublicDomain { get; set; }

        [JsonPropertyName("textToSpeechPermission")]
        public string? TextToSpeechPermission { get; set; }

        [JsonPropertyName("epub")]
        public virtual Epub? Epub { get; set; }

        [JsonPropertyName("pdf")]
        public virtual Pdf? Pdf { get; set; }

        [JsonPropertyName("webReaderLink")]
        public string? WebReaderLink { get; set; }

        [JsonPropertyName("accessViewStatus")]
        public string? AccessViewStatus { get; set; }

        [JsonPropertyName("quoteSharingAllowed")]
        public bool? QuoteSharingAllowed { get; set; }
        public virtual BookData? BookData { get; set; }
    }

    public class SearchInfo
    {
        [ForeignKey("BookData")]
        [JsonPropertyName("searchInfoId")]
        public Guid Id { get; set; }
        [JsonPropertyName("textSnippet")]
        public string? TextSnippet { get; set; }

        public virtual BookData? BookData { get; set; }
    }

    public class BookData
    {
        [JsonPropertyName("kind")]
        public string? Kind { get; set; }
        [JsonPropertyName("bookId")]
        public Guid BookId { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("etag")]
        public string? Etag { get; set; }

        [JsonPropertyName("selfLink")]
        public string? SelfLink { get; set; }

        public virtual VolumeInfo? VolumeInfo { get; set; }

        public virtual SaleInfo? SaleInfo { get; set; }

        public virtual AccessInfo? AccessInfo { get; set; }

        public virtual SearchInfo? SearchInfo { get; set; }
    }
}
