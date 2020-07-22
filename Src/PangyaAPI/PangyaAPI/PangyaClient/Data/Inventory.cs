using System;
using System.Runtime.CompilerServices;

namespace PangyaAPI.PangyaClient.Data
{
    public abstract class Inventory
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
        // transaction
        public abstract byte[] GetTransaction();
        public abstract byte[] GetDecorationData();
        public abstract byte[] GetGolfEQP();
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
        public abstract uint GetItemGroup(uint TypeID);

        public abstract uint DaysBetween(DateTime? d1, DateTime d2);

        public abstract uint GetTitleTypeID();
        public abstract uint GetCharTypeID();

        public abstract uint GetCutinIndex();


        public abstract uint GetMascotTypeID();
        public abstract uint GetQuantity(uint TypeId);

        uint GetPartGroup(uint TypeID)
        {
            uint result;
            result = (uint)Math.Round((TypeID & 4227858432) / Math.Pow(2.0, 26.0));
            return result;
        }
        #endregion

        #region Methods GetItem
        public abstract PlayerItemData GetUCC(uint ItemIdx);

        //THIS IS USE OR UCC THAT ALREADY PAINTED
        public abstract PlayerItemData GetUCC(uint TypeId, string UCC_UNIQUE, bool Status);

        //THIS IS USE OR UCC THAT ALREADY {NOT PAINTED}
        public abstract PlayerItemData GetUCC(uint TypeID, string UCC_UNIQUE);

        public abstract PlayerCharacterData GetCharacter(uint TypeID);
        #endregion

        #region Methods AddItems
        public abstract AddData AddItem(AddItem ItemAddData);

        public abstract AddData AddRent(uint TypeID, ushort Day = 7);

        public abstract AddData AddItemToDB(AddItem ItemAddData);
        #endregion

        #region RemoveItems
        public abstract AddData Remove(uint ItemIffId, uint Quantity, bool Transaction = true);

        public abstract AddData Remove(uint ItemIffId, uint Index, uint Quantity, bool Transaction = true);
        #endregion
    }
}
