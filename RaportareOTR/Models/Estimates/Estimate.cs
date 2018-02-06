using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RaportareOTR.Models.Estimates
{
    public class Estimate
    {
        public Estimate()
        {
            CreationDate = DateTime.Now;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }

        [ForeignKey("AspNetUsers")]
        [StringLength(450)]
        public string UserId { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public DateTime CreationDate { get; set; }

        // Quantity fields
        public double Total { get; set; }

        public double PaperCarton { get; set; }

        public double StickerPaperCarton { get; set; }

        public double TetraPakPaperCarton { get; set; }

        public double Wood { get; set; }

        public double Glass { get; set; }

        public double Metal { get; set; }

        public double MetalSteel { get; set; }

        public double MetalAl { get; set; }

        public double Plastic { get; set; }

        public double PlasticPET { get; set; }

        public double PlasticPE { get; set; }

        public double PlasticPVC { get; set; }

        public double PlasticPP { get; set; }

        public double PlasticPS { get; set; }

        public double PlasticOthers { get; set; }

        // Product quantities
        public double BatteryQuantity { get; set; }

        public double OilQuantity { get; set; }

        public double DangerousSubstances { get; set; }

        public double WheelsQuantity { get; set; }
        
        public double AppliancesQuantity { get; set; }

        public double BagsQuantity { get; set; }
       
        public double BigHouseHoldAppliances { get; set; }

        public double SmallHouseHoldAppliances { get; set; }

        public double InformaticEquipment { get; set; }

        public double ElectronicEquipment { get; set; }

        public double LightingEquipment { get; set; }

        public double ElectricalTools { get; set; }

        public double ElectricalToys { get; set; }

        public double MedicalDevices { get; set; }

        public double MonitoringInstruments { get; set; }

        public double AutomatedDistributors { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
