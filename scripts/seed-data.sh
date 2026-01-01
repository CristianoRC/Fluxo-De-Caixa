#!/bin/bash

# =============================================================================
# Script para gerar massa de dados de teste - Fluxo de Caixa
# =============================================================================

API_URL="${API_URL:-http://localhost:8081}"

# Cores para output
GREEN='\033[0;32m'
BLUE='\033[0;34m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

echo -e "${BLUE}========================================${NC}"
echo -e "${BLUE}  Gerando Massa de Dados de Teste${NC}"
echo -e "${BLUE}========================================${NC}"
echo ""

# =============================================================================
# Criação dos Balances (Carteiras)
# =============================================================================

echo -e "${YELLOW}📦 Criando Balances...${NC}"

# Balance 1: Cristiano
CRISTIANO=$(curl -s -X POST "$API_URL/api/balance" \
  -H "Content-Type: application/json" \
  -d '{"name": "Cristiano"}' | jq -r '.id')

if [ "$CRISTIANO" != "null" ] && [ -n "$CRISTIANO" ]; then
  echo -e "${GREEN}  ✓ Balance 'Cristiano' criado: $CRISTIANO${NC}"
else
  echo "  ✗ Erro ao criar balance 'Cristiano'"
  exit 1
fi

# Balance 2: Usuario ABC
USUARIO_ABC=$(curl -s -X POST "$API_URL/api/balance" \
  -H "Content-Type: application/json" \
  -d '{"name": "Usuario ABC"}' | jq -r '.id')

if [ "$USUARIO_ABC" != "null" ] && [ -n "$USUARIO_ABC" ]; then
  echo -e "${GREEN}  ✓ Balance 'Usuario ABC' criado: $USUARIO_ABC${NC}"
else
  echo "  ✗ Erro ao criar balance 'Usuario ABC'"
  exit 1
fi

# Balance 3: Empresa XYZ
EMPRESA_XYZ=$(curl -s -X POST "$API_URL/api/balance" \
  -H "Content-Type: application/json" \
  -d '{"name": "Empresa XYZ"}' | jq -r '.id')

if [ "$EMPRESA_XYZ" != "null" ] && [ -n "$EMPRESA_XYZ" ]; then
  echo -e "${GREEN}  ✓ Balance 'Empresa XYZ' criado: $EMPRESA_XYZ${NC}"
else
  echo "  ✗ Erro ao criar balance 'Empresa XYZ'"
  exit 1
fi

# Balance 4: Fornecedor 123
FORNECEDOR=$(curl -s -X POST "$API_URL/api/balance" \
  -H "Content-Type: application/json" \
  -d '{"name": "Fornecedor 123"}' | jq -r '.id')

if [ "$FORNECEDOR" != "null" ] && [ -n "$FORNECEDOR" ]; then
  echo -e "${GREEN}  ✓ Balance 'Fornecedor 123' criado: $FORNECEDOR${NC}"
else
  echo "  ✗ Erro ao criar balance 'Fornecedor 123'"
  exit 1
fi

echo ""
echo -e "${YELLOW}💰 Criando Transações...${NC}"

# =============================================================================
# Função para criar transação
# =============================================================================

create_transaction() {
  local entry_balance=$1
  local offset_balance=$2
  local amount=$3
  local type=$4  # 0 = Debit, 1 = Credit
  local description=$5
  
  local result=$(curl -s -X POST "$API_URL/api/bookentry" \
    -H "Content-Type: application/json" \
    -d "{\"entryBalance\":\"$entry_balance\",\"offsetBalance\":\"$offset_balance\",\"amount\":$amount,\"transactionType\":$type,\"description\":\"$description\"}")
  
  local id=$(echo $result | jq -r '.id')
  if [ "$id" != "null" ] && [ -n "$id" ]; then
    echo -e "${GREEN}  ✓ Transação criada: $description (R$ $amount)${NC}"
  else
    echo "  ✗ Erro ao criar transação: $description"
  fi
}

# =============================================================================
# Transações entre Cristiano e Usuario ABC
# =============================================================================

echo -e "\n${BLUE}  → Transações: Cristiano ↔ Usuario ABC${NC}"
create_transaction "$CRISTIANO" "$USUARIO_ABC" 100.00 1 "Pagamento de serviços"
create_transaction "$USUARIO_ABC" "$CRISTIANO" 50.00 0 "Devolução parcial"
create_transaction "$CRISTIANO" "$USUARIO_ABC" 200.00 1 "Compra de materiais"
create_transaction "$USUARIO_ABC" "$CRISTIANO" 75.00 1 "Reembolso de despesas"
create_transaction "$CRISTIANO" "$USUARIO_ABC" 150.00 0 "Transferência mensal"

# =============================================================================
# Transações entre Cristiano e Empresa XYZ
# =============================================================================

echo -e "\n${BLUE}  → Transações: Cristiano ↔ Empresa XYZ${NC}"
create_transaction "$CRISTIANO" "$EMPRESA_XYZ" 500.00 1 "Contrato de consultoria"
create_transaction "$EMPRESA_XYZ" "$CRISTIANO" 250.00 0 "Pagamento parcial"
create_transaction "$CRISTIANO" "$EMPRESA_XYZ" 180.00 1 "Serviços adicionais"

# =============================================================================
# Transações entre Usuario ABC e Empresa XYZ
# =============================================================================

echo -e "\n${BLUE}  → Transações: Usuario ABC ↔ Empresa XYZ${NC}"
create_transaction "$USUARIO_ABC" "$EMPRESA_XYZ" 300.00 1 "Investimento inicial"
create_transaction "$EMPRESA_XYZ" "$USUARIO_ABC" 120.00 1 "Dividendos Q1"
create_transaction "$USUARIO_ABC" "$EMPRESA_XYZ" 85.00 0 "Taxa administrativa"

# =============================================================================
# Transações com Fornecedor 123
# =============================================================================

echo -e "\n${BLUE}  → Transações: Todos ↔ Fornecedor 123${NC}"
create_transaction "$CRISTIANO" "$FORNECEDOR" 450.00 0 "Compra de equipamentos"
create_transaction "$USUARIO_ABC" "$FORNECEDOR" 220.00 0 "Materiais de escritório"
create_transaction "$EMPRESA_XYZ" "$FORNECEDOR" 800.00 0 "Estoque mensal"
create_transaction "$FORNECEDOR" "$CRISTIANO" 45.00 1 "Desconto por volume"

# =============================================================================
# Resumo Final
# =============================================================================

echo ""
echo -e "${BLUE}========================================${NC}"
echo -e "${BLUE}  Resumo dos Balances${NC}"
echo -e "${BLUE}========================================${NC}"

curl -s "$API_URL/api/balance" | jq -r '.[] | "  \(.name): R$ \(.amount.value)"'

echo ""
echo -e "${GREEN}✅ Massa de dados gerada com sucesso!${NC}"
echo -e "${GREEN}   - 4 Balances criados${NC}"
echo -e "${GREEN}   - 15 Transações criadas${NC}"
