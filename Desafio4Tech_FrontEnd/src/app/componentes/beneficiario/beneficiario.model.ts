export interface Beneficiario {
  id?: number;
  nomeCompleto: string;
  cpf: string;
  dataNascimento: string; 
  status: 'ATIVO' | 'INATIVO';
  planoId: number;
  plano: string;
  dataExclusao?: string; 
}
