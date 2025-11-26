export interface Beneficiario {
  id?: number;
  nome_completo: string;
  cpf: string;
  data_nascimento: string; 
  status: 'ATIVO' | 'INATIVO';
  planoId: number;
  plano: string;
}
