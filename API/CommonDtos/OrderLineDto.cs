﻿namespace API.CommonDtos
{
    public class OrderLineDto
    {
        public string ProductCode { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public string ProductPrice { get; set; } = string.Empty;
        public string ProductCategory { get; set; } = string.Empty;
    }
}
