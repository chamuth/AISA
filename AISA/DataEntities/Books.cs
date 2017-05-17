using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace AISA.DataEntities
{
    [XmlRoot(ElementName = "id")]
    public class Id
    {
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "books_count")]
    public class Books_count
    {
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "ratings_count")]
    public class Ratings_count
    {
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "text_reviews_count")]
    public class Text_reviews_count
    {
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "original_publication_year")]
    public class Original_publication_year
    {
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "original_publication_month")]
    public class Original_publication_month
    {
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "original_publication_day")]
    public class Original_publication_day
    {
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "author")]
    public class Author
    {
        [XmlElement(ElementName = "id")]
        public Id Id { get; set; }
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
    }

    [XmlRoot(ElementName = "best_book")]
    public class Best_book
    {
        [XmlElement(ElementName = "id")]
        public Id Id { get; set; }
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }
        [XmlElement(ElementName = "author")]
        public Author Author { get; set; }
        [XmlElement(ElementName = "image_url")]
        public string Image_url { get; set; }
        [XmlElement(ElementName = "small_image_url")]
        public string Small_image_url { get; set; }
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
    }

    [XmlRoot(ElementName = "work")]
    public class Work
    {
        [XmlElement(ElementName = "id")]
        public Id Id { get; set; }
        [XmlElement(ElementName = "books_count")]
        public Books_count Books_count { get; set; }
        [XmlElement(ElementName = "ratings_count")]
        public Ratings_count Ratings_count { get; set; }
        [XmlElement(ElementName = "text_reviews_count")]
        public Text_reviews_count Text_reviews_count { get; set; }
        [XmlElement(ElementName = "original_publication_year")]
        public Original_publication_year Original_publication_year { get; set; }
        [XmlElement(ElementName = "original_publication_month")]
        public Original_publication_month Original_publication_month { get; set; }
        [XmlElement(ElementName = "original_publication_day")]
        public Original_publication_day Original_publication_day { get; set; }
        [XmlElement(ElementName = "average_rating")]
        public string Average_rating { get; set; }
        [XmlElement(ElementName = "best_book")]
        public Best_book Best_book { get; set; }
    }

    [XmlRoot(ElementName = "results")]
    public class Results
    {
        [XmlElement(ElementName = "work")]
        public Work Work { get; set; }
    }

    [XmlRoot(ElementName = "search")]
    public class Search
    {
        [XmlElement(ElementName = "query")]
        public string Query { get; set; }
        [XmlElement(ElementName = "results-start")]
        public string Resultsstart { get; set; }
        [XmlElement(ElementName = "results-end")]
        public string Resultsend { get; set; }
        [XmlElement(ElementName = "total-results")]
        public string Totalresults { get; set; }
        [XmlElement(ElementName = "source")]
        public string Source { get; set; }
        [XmlElement(ElementName = "query-time-seconds")]
        public string Querytimeseconds { get; set; }
        [XmlElement(ElementName = "results")]
        public Results Results { get; set; }
    }

    [XmlRoot(ElementName = "GoodreadsResponse")]
    public class GoodreadsResponse
    {
        [XmlElement(ElementName = "Request")]
        public string Request { get; set; }
        [XmlElement(ElementName = "search")]
        public Search Search { get; set; }
    }

}

