using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Satelite
{
    static class Variables
    {      
        public static string INIInstall { get; set; }
        public static string Alicia { get; set; }
        public static int Voz { get; set; }
        public static string R { get; set; }
        public static string Inno { get; set; }
        public static string Python { get; set; }
        //tipo es la conexion con la que se conecta a base de Datos, la 1 es la propia
        public static int tipoorigen { get; set; }               //  Configuracion para creacion de tablas
        public static int tipodestino { get; set; }               //  Configuracion para creacion de tablas
        public static int tipo { get; set; }               //  Configuracion para creacion de tablas
        public static int configuracion { get; set; }      //0 acceso a Elexis  
        public static int tipo2 { get; set; }              //1 Con acceso a base de datos pero sin Tablas 
        public static int nivel { get; set; }              //2 Correcto acceso a Base de Datos y Tablas 
        public static int idcampo { get; set; }
        public static long Peso { get; set; }
        public static int configuracionDB { get; set; }
        public static int idarchivo { get; set; }
        public static int iddirectorio { get; set; }
        public static int edicion { get; set; }
        public static int enEspera { get; set; }
        public static int enEnvio { get; set; }
        public static string idusuario { get; set; }
        public static Boolean pks { get; set; }
        public static string Login { get; set; }
        public static string instalacion { get; set; }
        public static string pki1 { get; set; }
        public static string pki2 { get; set; }
        public static string pki0 { get; set; }
        public static string pkiM { get; set; }
        public static string sessionD { get; set; }
        public static string Portal { get; set; }
        public static string grupo { get; set; }
        public static string usuario { get; set; }
        public static string usuarioSO { get; set; }
        public static string password { get; set; }
        public static string nomenclatura { get; set; }
        public static string mensajeserver { get; set; }
        public static int id { get; set; }
        public static string key { get; set; }
        public static string descripcion { get; set; }
        public static string OrigConexion { get; set; }
        public static string DestConexion { get; set; }
        public static string Miconexion { get; set; }
        public static string MiFormula { get; set; }
        public static string MiResultado { get; set; }
        public static int MiWidth { get; set; }
        public static int MiHeight { get; set; }
        public static string GetData { get; set; }
        public static string Ip { get; set; }
        public static string Port { get; set; }
        public static string Error { get; set; }
        public static string Formulario { get; set; }
        public static string Filelog { get; set; }

        //Botones
        public static string mConversionA { get; set; }
        public static string mConversionB { get; set; }
        public static string MMVariable { get; set; }
        public static string MMVariableB { get; set; }
        public static string MMVariableC { get; set; }
        public static string MMVariableD { get; set; }
        public static string mCadena { get; set; }
        public static string mEXEa { get; set; }
        public static string mEXEb { get; set; }
        public static string mFORMa { get; set; }
        public static string mFORMb { get; set; }
        public static string mDEFINESa { get; set; }
        public static string mDEFINESb { get; set; }
        public static string mSETUPSa { get; set; }
        public static string mSETUPSb { get; set; }
        public static string mIDIOMASa { get; set; }
        public static string mIDIOMASb { get; set; }
        public static string mCUSTASKSa { get; set; }
        public static string mCUSTASKSb { get; set; }
        public static string mADDFILESa { get; set; }
        public static string mADDFILESb { get; set; }
        public static string mICONPACKSa { get; set; }
        public static string mICONPACKSb { get; set; }
        public static string mRUNSa { get; set; }
        public static string mRUNSb { get; set; }
        public static string mPATHa { get; set; }
        public static string mTEXTOa { get; set; }
        public static string mTEXTOb { get; set; }
        public static string mUPLOADa { get; set; }
        public static string mUPLOADb { get; set; }
        public static string mPATHb { get; set; }
        public static string[] mFILEsA { get; set; }
        public static string[] mFILEsB { get; set; }
        public static string mFILEa { get; set; }
        public static string mFILEb { get; set; }
        public static string mParamFILEa { get; set; }
        public static string mParamFILEb { get; set; }
        public static string mCOPIA { get; set; }
        public static string mLITERALa { get; set; }
        public static string mLITERALb { get; set; }
        public static string mDESCRIPCIONa { get; set; }
        public static string mDESCRIPCIONb { get; set; }
        public static string mMOVE { get; set; }
        public static string mDELETE { get; set; }
        public static string mEXECUTECMDa { get; set; }
        public static string mEXECUTECMDb { get; set; }
        public static string mCMD { get; set; }
        public static string mZIPa { get; set; }
        public static string mZIPb { get; set; }
        public static string mINCREMposCeldaa { get; set; }
        public static string mINCREMENTO { get; set; }
        public static string mINCREMENTOafi { get; set; }
        public static string mINCREMENTObfi { get; set; }
        public static string mINCREMENTOaff { get; set; }
        public static string mINCREMENTObff { get; set; }
        public static string mINCREMENTOaci { get; set; }
        public static string mINCREMENTObci { get; set; }
        public static string mINCREMENTOacf { get; set; }
        public static string mINCREMENTObcf { get; set; }
        public static string mINCREMENTOai { get; set; }
        public static string mINCREMENTObi { get; set; }
        public static string mMACRO { get; set; }
        public static string mCLOSE { get; set; }
        public static string mINSTALL { get; set; }
        public static string mMAKEDIRa { get; set; }
        public static string mMAKEDIRb { get; set; }
        public static string mFECHA { get; set; }
        public static string mBBDDa { get; set; }
        public static string mBBDDb { get; set; }
        public static string mSRVDATOSa { get; set; }
        public static string mSRVDATOSb { get; set; }
        public static string mTIPODATOSa { get; set; }
        public static string mTIPODATOSb { get; set; }
        public static string mBASEDATOSa { get; set; }
        public static string mBASEDATOSb { get; set; }
        public static string mUSERa { get; set; }
        public static string mUSERb { get; set; }
        public static string mPASSa { get; set; }
        public static string mPASSb { get; set; }
        public static string mDSNa { get; set; }
        public static string mDSNb { get; set; }
        public static string mBBDDQa { get; set; }
        public static string mBBDDQb { get; set; }
        public static string mSQLa { get; set; }
        public static string mSQLb { get; set; }
        public static string mCORREO { get; set; }
        public static string mMAIL { get; set; }
        public static string mOCR { get; set; }
        public static string mWHILE { get; set; }
        public static string mHOJAa { get; set; }
        public static string mHOJAb { get; set; }
        public static string mRANGOPASTEa { get; set; }
        public static string mRANGOPASTEb { get; set; }
        public static string mRANGOa { get; set; }
        public static string mRANGOb { get; set; }
        public static string mCELDAa { get; set; }
        public static string mCELDAb { get; set; }
        public static string mALTERNATERGBa1 { get; set; }
        public static string mALTERNATERGBa2 { get; set; }
        public static string mALTERNATERGBb1 { get; set; }
        public static string mALTERNATERGBb2 { get; set; }
        public static string mEJESa { get; set; }
        public static string mEJESb { get; set; }
        public static string mIMAGENa { get; set; }
        public static string mIMAGENb { get; set; }
        public static string mMARCADORa{ get; set; }
        public static string mMARCADORb { get; set; }
        public static string mPegaDatoa { get; set; }
        public static string mPegaDatob { get; set; }
        public static string mCOMPARE { get; set; }
        public static string mSUMAR { get; set; }
        public static string mSUMAT { get; set; }
        public static string mSUMATOTAL { get; set; }
        public static string mTRANSPONER { get; set; }
        public static string mCOLORa { get; set; }
        public static string mCOLORb { get; set; }
        public static string mClickIzqDown { get; set; }
        public static string mClickIzqUp { get; set; }
        public static string mClickCentDown { get; set; }
        public static string mClickCentUp { get; set; }
        public static string mClickDerDown { get; set; }
        public static string mClickDerUp { get; set; }
        public static string mClickMove { get; set; }
        public static string mClickScroll { get; set; }
        public static string mClickKeyDown { get; set; }
        public static string mClickKeyUp { get; set; }
        public static string mREPLACE { get; set; }
        public static string mClickKeyCon { get; set; }
        public static string mPUERTOa { get; set; }
        public static string mPUERTOb { get; set; }
        public static string mAUTENTICATE { get; set; }
        public static string mSSL { get; set; }
        public static string mCUSER { get; set; }
        public static string mCPASS { get; set; }
        public static string mASUNTO { get; set; }
        public static string mDE { get; set; }
        public static string mPARA { get; set; }
        public static string mADJUNTO { get; set; }
        public static string mSOURCEa { get; set; }
        public static string mSOURCEb { get; set; }
        public static string mANO { get; set; }
        public static string mANOa { get; set; }
        public static string mANOb { get; set; }
        public static string mMES { get; set; }
        public static string mMESa { get; set; }
        public static string mMESb { get; set; }
        public static string mDIA { get; set; }
        public static string mDIAa { get; set; }
        public static string mDIAb { get; set; }
        public static string mALICIAa { get; set; }
        public static string mALICIAb { get; set; }
        public static string mSECUENCIAa { get; set; }
        public static string mSECUENCIAb { get; set; }
        public static string MiColor { get; set; }
        
    }
}
