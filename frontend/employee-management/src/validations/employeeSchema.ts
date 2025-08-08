import { z } from 'zod';

function isValidCPF(cpf: string) {
  cpf = cpf.replace(/[^\d]+/g, '');
  if (cpf.length !== 11 || /^(\d)\1+$/.test(cpf)) return false;
  let sum = 0, rest;
  for (let i = 1; i <= 9; i++) sum += parseInt(cpf[i - 1]) * (11 - i);
  rest = (sum * 10) % 11;
  if (rest === 10 || rest === 11) rest = 0;
  if (rest !== parseInt(cpf[9])) return false;
  sum = 0;
  for (let i = 1; i <= 10; i++) sum += parseInt(cpf[i - 1]) * (12 - i);
  rest = (sum * 10) % 11;
  if (rest === 10 || rest === 11) rest = 0;
  return rest === parseInt(cpf[10]);
}

export const employeeSchema = z.object({
  fullName: z.string().min(3, 'Nome completo deve ter pelo menos 3 caracteres'),
  email: z.string().email('E-mail inválido'),
  birthDate: z.string().nonempty('Data de nascimento obrigatória'),

  docNumber: z
    .string()
    .nonempty({ message: 'CPF obrigatório' })
    .max(14, { message: 'CPF deve ter no máximo 14 caracteres (com pontuação)' })
    .regex(/^\d{3}\.\d{3}\.\d{3}-\d{2}$/, {
      message: 'CPF inválido. Use o formato 000.000.000-00',
    })
    .refine((cpf) => isValidCPF(cpf), {
      message: 'CPF inválido (dígitos verificadores incorretos)',
    }),

  phones: z.array(
    z
      .string()
      .nonempty('Telefone obrigatório')
      .regex(/^\(\d{2}\) \d{5}-\d{4}$/, 'Formato inválido: (00) 00000-0000')
  ),

  position: z.string().nonempty('Cargo obrigatório'),

  salary: z
    .number()
    .min(0, 'Salário não pode ser negativo')
    .or(z.literal(0)),

  managerId: z.number().nullable(),
});
