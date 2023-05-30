using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Mobiles
{
    public int ID { get; set; }
    public string? Brand { get; set; }
    public string? Model { get; set; } = null!;
    public string? Announced { get; set; }
    public bool? Audio_jack { get; set; }
    public string? Battery { get; set; }
    public string? Bluetooth { get; set; }
    public string? CPU { get; set; }
    public string? Chipset { get; set; }
    public string? Colors { get; set; }
    public string? Dimensions { get; set; }
    public string? Display_resolution { get; set; }
    public string? Display_size { get; set; }
    public string? Display_type { get; set; }
    public string? EDGE { get; set; }
    public string? FourG { get; set; }
    public string? GPRS { get; set; }
    public string? GPS { get; set; }
    public string? GPU { get; set; }
    public string? Internal_memory { get; set; }
    public string? Loud_speaker { get; set; }
    public string? Memory_card { get; set; }
    public string? NFC { get; set; }
    public string? Network { get; set; }
    public string? Network_Speed { get; set; }
    public string? Operating_System { get; set; }
    public string? Primary_camera { get; set; }
    public string? RAM { get; set; }
    public string? Radio { get; set; }
    public string? SIM { get; set; }
    public string? Secondary_camera { get; set; }
    public string? Sensors { get; set; }
    public string? Status { get; set; }
    public string? ThreeG { get; set; }
    public string? TwoG { get; set; }
    public string? USB { get; set; }
    public string? WLAN { get; set; }
}