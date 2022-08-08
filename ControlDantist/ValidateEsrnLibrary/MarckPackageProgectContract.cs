using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlDantist.DataBaseContext;

namespace ControlDantist.ValidateEsrnLibrary
{
    /// <summary>
    /// Помечает договра прошедшие проверку.
    /// </summary>
    public class MarckPackageProgectContract
    {
        // Пункст из договора.
        private ItemLibrary itemLibrary;

        public MarckPackageProgectContract(ItemLibrary itemLibrary)
        {
            this.itemLibrary = itemLibrary ?? throw new ArgumentNullException(nameof(itemLibrary));
        }

        /// <summary>
        /// Установка маре=кера прошедшего проверку.
        /// </summary>
        public void SetMarck()
        {
            if (itemLibrary.DiscriptionValidate != null)
            {
                if(this.itemLibrary.DiscriptionValidate.GetFlag() == true)
                {
                    // Пометим льготника как прошедшего проверку по ЭСРН.
                    this.itemLibrary.FlagValidateEsrn = true;

                }
                else
                {
                    this.itemLibrary.FlagValidateEsrn = false;
                }
            }
            else
            {
                this.itemLibrary.DiscriptionValidate = new DiscriptionValidate();
                this.itemLibrary.DiscriptionValidate.SetFlag(false, "Льготник не найден в ЭСРН; ");
                this.itemLibrary.DiscriptionValidate.SetFlagPassword(false, " Не найдены сведения о пасорте в ЭСРН; ");

                itemLibrary.FlagValidateEsrn = false;
            }

        }
               
    }
}
