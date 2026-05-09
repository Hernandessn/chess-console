# ♟️ Chess Console

> A fully functional chess game running in the terminal, built with C#.

---

## 🇧🇷 Português

### Sobre o Projeto

Jogo de xadrez completo desenvolvido como aplicação console em C#, com todas as peças, regras e jogadas especiais implementadas.

### Funcionalidades

- Tabuleiro renderizado no terminal com cores
- Todas as peças com movimentos corretos: Rei, Dama, Torre, Bispo, Cavalo e Peão
- Destaque visual das posições possíveis de movimento
- Controle de turno entre jogador branco e amarelo
- Controle de peças capturadas
- Verificação de xeque e xeque-mate
- **Jogadas especiais:**
  - Roque pequeno e roque grande
  - En passant
  - Promoção do peão
- Tratamento de erros com exceção personalizada (`BoardException`)
- Validação de input do usuário

### Tecnologias

- C# / .NET
- Aplicação Console

### Como Executar

```bash
# Clone o repositório
git clone https://github.com/Hernandessn/chess-console.git

# Acesse a pasta do projeto
cd chess-console

# Execute o projeto
dotnet run
```

### Como Jogar

1. Digite a posição de **origem** da peça (ex: `e2`)
2. As posições possíveis serão destacadas em cinza no tabuleiro
3. Digite a posição de **destino** (ex: `e4`)
4. O turno passa para o outro jogador
5. O jogo termina quando houver xeque-mate

### Estrutura do Projeto

```
chess-console/
├── Board/
│   ├── Board.cs          # Tabuleiro
│   ├── Piece.cs          # Peça base (abstrata)
│   ├── Position.cs       # Posição matricial
│   ├── Color.cs          # Enum de cores
│   └── BoardException.cs # Exceção personalizada
├── chess/
│   ├── ChessGame.cs      # Lógica principal do jogo
│   ├── PositionChess.cs  # Posição no formato xadrez (ex: e2)
│   ├── King.cs           # Rei
│   ├── Queen.cs          # Dama
│   ├── Tower.cs          # Torre
│   ├── Bishop.cs         # Bispo
│   ├── Horse.cs          # Cavalo
│   └── Pawn.cs           # Peão
├── Screen.cs             # Renderização do tabuleiro
└── Program.cs            # Ponto de entrada
```

---

## 🇺🇸 English

### About

A fully functional chess game built as a C# console application, featuring all pieces, rules, and special moves.

### Features

- Terminal-rendered board with colors
- All pieces with correct movement rules: King, Queen, Rook, Bishop, Knight and Pawn
- Visual highlight of possible move positions
- Turn control between white and yellow players
- Captured pieces tracking
- Check and checkmate verification
- **Special moves:**
  - Kingside and queenside castling
  - En passant
  - Pawn promotion
- Custom error handling via `BoardException`
- User input validation

### Technologies

- C# / .NET
- Console Application

### How to Run

```bash
# Clone the repository
git clone https://github.com/Hernandessn/chess-console.git

# Navigate to the project folder
cd chess-console

# Run the project
dotnet run
```

### How to Play

1. Enter the **origin** position of the piece (e.g. `e2`)
2. Possible move positions will be highlighted in gray on the board
3. Enter the **destination** position (e.g. `e4`)
4. The turn passes to the other player
5. The game ends when checkmate is reached

### Project Structure

```
chess-console/
├── Board/
│   ├── Board.cs          # Game board
│   ├── Piece.cs          # Abstract base piece
│   ├── Position.cs       # Matrix position
│   ├── Color.cs          # Color enum
│   └── BoardException.cs # Custom exception
├── chess/
│   ├── ChessGame.cs      # Core game logic
│   ├── PositionChess.cs  # Chess-notation position (e.g. e2)
│   ├── King.cs
│   ├── Queen.cs
│   ├── Tower.cs          # Rook
│   ├── Bishop.cs
│   ├── Horse.cs          # Knight
│   └── Pawn.cs
├── Screen.cs             # Board rendering
└── Program.cs            # Entry point
```

---

Developed as part of a C# course on Udemy.
