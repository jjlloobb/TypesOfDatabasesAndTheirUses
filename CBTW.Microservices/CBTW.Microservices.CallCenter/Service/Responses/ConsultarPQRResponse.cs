﻿namespace CBTW.Microservices.CallCenter.Service.Responses;

public class ConsultarPQRResponse
{
    public long Id { get; set; }

    public long IdCustomer { get; set; }

    public string Subject { get; set; }

    public string Description { get; set; }
}
