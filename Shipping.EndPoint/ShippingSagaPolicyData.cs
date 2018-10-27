﻿using NServiceBus;

namespace Shipping.EndPoint
{
    public class ShippingSagaPolicyData : ContainSagaData
    {
        public string OrderId { get; set; }
        public bool IsOrderPlaced { get; set; }
        public bool IsOrderBilled { get; set; }
    }
}
