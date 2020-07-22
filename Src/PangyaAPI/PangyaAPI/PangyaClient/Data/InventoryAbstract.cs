using System;
using System.Runtime.CompilerServices;

namespace PangyaAPI.PangyaClient.Data
{
    public abstract class InventoryAbstract
    {
        #region Methods Array of Byte
        public abstract byte[] GetToolbar();
        public abstract byte[] GetCharData();
        public abstract byte[] GetCharData(uint Index);
        public abstract byte[] GetMascotData();
        public abstract byte[] GetTrophyInfo();
        public abstract byte[] GetEquipData();
        public abstract byte[] GetClubData();
        public abstract byte[] GetCaddieData();

        public abstract byte[] GetTransaction();
        public abstract byte[] GetDecorationData();
        public abstract byte[] GetGolfEQP();

        public abstract byte[] GetEquipInfo();
        #endregion

        #region Methods Bool
        // poster
        public abstract bool SetCutInIndex(uint CharIndex, uint CutinIndex);
        public abstract bool SetPoster(uint Poster1, uint Poster2);

        public abstract bool IsExist(uint TypeID, uint Index, uint Quantity);

        // item exists?
        public abstract bool IsExist(uint TypeId);

        public abstract bool Available(uint TypeID, uint Quantity);

        public abstract bool SetMascotText(uint MascotIdx, string MascotText);
        // caddie system
        public abstract bool SetCaddieIndex(uint Index);

        // mascot system
        public abstract bool SetMascotIndex(uint Index);

        public abstract bool SetCharIndex(uint CharID);

        public abstract bool SetBackgroudIndex(uint typeID);

        public abstract bool SetFrameIndex(uint typeID);


        public abstract bool SetStickerIndex(uint typeID);
        public abstract bool SetSlotIndex(uint typeID);

        public abstract bool SetTitleIndex(uint ID);
        public abstract bool SetDecoration(uint background, uint frame, uint sticker, uint slot, uint un, uint title);

        // club system
        public abstract bool SetClubSetIndex(uint Index);

        public abstract bool SetGolfEQP(uint BallTypeID, uint ClubSetIndex);
        public abstract bool SetBallTypeID(uint TypeID);

        #endregion

        #region Methods UInt32
        [MethodImpl(MethodImplOptions.NoInlining)]
        public uint GetItemGroup(uint TypeID)
        {
            var result = (uint)Math.Round((TypeID & 4227858432) / Math.Pow(2.0, 26.0));

            return result;
        }

        public uint DaysBetween(DateTime? d1, DateTime d2)
        {
            TimeSpan span = d1.Value.Subtract(d2);
            return Convert.ToUInt32(span.Days);
        }

        public abstract uint GetTitleTypeID();
        public abstract uint GetCharTypeID();

        public abstract uint GetCutinIndex();

        public abstract uint GetMascotTypeID();
        public abstract uint GetQuantity(uint TypeId);

        #endregion       
    }
}
