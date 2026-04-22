using ApiLogin.Models;
using System.Xml.Linq;

namespace ApiLogin.Infraestructure.Data
{
    public class PASE
    {
        public enum NSQLASE
        {
            #region ASE
            Empleado_Expediente = 1,
            CC_Empleados = 2,
            CC_Todos_Empleados = 3,
            Empleado_Usuario = 4,
            Buque_Llegada = 5,
            Emp_Exp_Foto = 6,
            #endregion
            //Continuar al definir las opciones restantes
        }

        public static string SQLASE(NSQLASE numero, string[] args)
        {
            string sentencia = "";
            switch ((int)numero)
            {
                case 1:
                    sentencia = @"select a.c_expediente as Expediente, a.s_nombres as Nombres, a.s_prim_ape + ' ' + 
                                case a.s_ape_casada when null then a.s_seg_ape when '' then a.s_seg_ape else ' DE ' 
                                + a.s_ape_casada end as Apellidos, a.c_alfanumerico as Alfa, a.c_cen_cos as Ccostos, 
                                (SELECT d_cen_cos FROM cn_cen_cos where c_empresa = '04' and c_cen_cos = a.c_cen_cos) 
                                as Nombrecc, case a.n_sexo when 2 then 'MASCULINO' else 'FEMENINO' end as Sexo  
                                from pl_expediente as a 
                                where a.c_empresa = '04' and a.c_expediente = '" + args[0] + "' and a.b_activo = '1'";
                    break;
                case 2:
                    sentencia = @"select a.c_expediente as Expediente, a.s_nombres as Nombres, a.s_prim_ape+' '+
                                case a.s_ape_casada when null then a.s_seg_ape when '' then a.s_seg_ape else ' DE ' 
                                + a.s_ape_casada end as Apellidos, a.c_alfanumerico as Alfa, a.c_cen_cos as Ccostos, 
                                (SELECT d_cen_cos FROM cn_cen_cos where c_empresa = '04' and c_cen_cos = a.c_cen_cos) 
                                as Nombrecc, case a.n_sexo when 2 then 'MASCULINO' else 'FEMENINO' end as Sexo 
                                from pl_expediente as a where a.c_empresa = '04' 
                                and a.c_cen_cos = '" + args[0] + "' and a.b_activo = '1' order by a.c_cen_cos, Apellidos, Nombres";
                    break;
                case 3:
                    sentencia = @"select a.c_expediente as Expediente, a.s_nombres as Nombres, a.s_prim_ape+' '+
                                case a.s_ape_casada when null then a.s_seg_ape when '' then a.s_seg_ape else ' DE ' 
                                + a.s_ape_casada end as Apellidos, a.c_alfanumerico as Alfa, a.c_cen_cos as Ccostos, 
                                (SELECT d_cen_cos FROM cn_cen_cos where c_empresa = '04' and c_cen_cos = a.c_cen_cos) 
                                as Nombrecc, case a.n_sexo when 2 then 'MASCULINO' else 'FEMENINO' end as Sexo 
                                from pl_expediente as a where a.c_empresa = '04' and a.b_activo = '1' 
                                order by a.c_cen_cos, Apellidos, Nombres";
                    break;
                case 4:
                    sentencia = @"select a.c_expediente as Expediente, a.s_nombres as Nombres, a.s_prim_ape + ' ' + 
                                case a.s_ape_casada when null then a.s_seg_ape when '' then a.s_seg_ape else ' DE ' 
                                + a.s_ape_casada end as Apellidos, a.c_alfanumerico as Alfa, a.c_cen_cos as Ccostos, 
                                (SELECT d_cen_cos FROM cn_cen_cos where c_empresa = '04' and c_cen_cos = a.c_cen_cos) 
                                as Nombrecc, case a.n_sexo when 2 then 'MASCULINO' else 'FEMENINO' end as Sexo,
                                b.c_cargo as Ccargo, (select s_descripcion from pl_cargo where c_cargo = b.c_cargo) as Dcargo
                                FROM pl_expediente a inner join pl_sit_rev b on a.c_expediente = b.c_expediente
                                where a.c_empresa = '04' and rtrim(ltrim(a.c_id_usuario)) = '" + args[0] + "' and a.b_activo = '1' " +
                                "and a.b_sit_empre != '1' and b.e_activo = 1";
                    break;
                case 5:
                    sentencia = @"select a.s_nom_buque as nombre, c.f_fin_oper from ((fa_buques as a inner join fa_aviso_lleg as b on a.c_buque = b.c_buque) " +
                                "left join fa_llegadas as c on (b.c_buque = c.c_buque and b.c_llegada = c.c_llegada))" +
                                "where b.c_llegada = '" + args[0] + "' and b.c_empresa = '04'";
                    break;
                case 6:
                    sentencia = @"SET TEXTSIZE 64000000 select a.c_expediente as Expediente, a.s_nombres as Nombres, a.s_prim_ape + ' ' + 
                                case a.s_ape_casada when null then a.s_seg_ape when '' then a.s_seg_ape else ' DE ' 
                                + a.s_ape_casada end as Apellidos, a.c_alfanumerico as Alfa, a.c_cen_cos as Ccostos, 
                                (SELECT d_cen_cos FROM cn_cen_cos where c_empresa = '04' and c_cen_cos = a.c_cen_cos) 
                                as Nombrecc, case a.n_sexo when 2 then 'MASCULINO' else 'FEMENINO' end as Sexo, s_foto as Foto
                                from pl_expediente as a 
                                where a.c_empresa = '04' and rtrim(ltrim(a.c_id_usuario)) = '" + args[0] + "' and a.b_activo = '1'";
                    break;
                default:
                    sentencia = @"select distinct a.c_cen_cos as Ccostos, (SELECT d_cen_cos FROM cn_cen_cos where c_empresa = '04' 
                                and c_cen_cos = a.c_cen_cos) as Nombrecc from pl_expediente as a 
                                where a.c_empresa = '04' and a.b_activo = '1' order by a.c_cen_cos";
                    break;
            }
            return sentencia;
        }
    }

}
