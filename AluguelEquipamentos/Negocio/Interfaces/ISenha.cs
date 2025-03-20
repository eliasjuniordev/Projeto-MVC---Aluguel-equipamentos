namespace AluguelEquipamentos.Negocio.Interfaces
{
    public interface ISenha
    {
        void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt);
        bool VerificaSenha(string senha, byte[] senhaHash, byte[] senhaSalt);
    }
}
