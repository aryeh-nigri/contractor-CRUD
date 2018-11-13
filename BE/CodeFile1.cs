//Search if its better to do all enums in a class Enums, 
//or make enums in an empty file, outside any class.

public enum Test
{
    General,
    DB,
    Community,
    Crypto,
    ServerSide,
    ClientSide,
    Mobile,
    UI
};


/*
  מבנה המתאר חשבון בנק המכיל:
מספר בנק
שם הבנק
מספר סניף 
כתובת הסניף
עיר הסניף
מספר חשבו
*/
public struct BankAccount
{
    public int bankNumber;
    public string bankName;
    public int bankAgency;
    public string bankAddress;
    public string bankCity;
    public int accountNumber;
};


public enum Degree
{
    Certificate,
    Bachelor,
    Master,
    Doctor,
    Student
};



//8. מאפיין enum שיציין את תחום ההתעסקות של המעסיק, מקביל לתחום שיש במומחיות.
public enum Domain
{
    Aeronauthics,
    Military,
    Web
};

/*

 */
