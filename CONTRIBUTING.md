# Contributing to Moxfield Deck Optimizer

Thank you for your interest in contributing to the Moxfield Deck Optimizer! This document provides guidelines and information for contributors.

## Getting Started

1. Fork the repository
2. Clone your fork: `git clone https://github.com/your-username/Moxfield.Deck.Optimizer.git`
3. Create a feature branch: `git checkout -b feature/your-feature-name`
4. Make your changes
5. Run tests: `dotnet test`
6. Commit your changes: `git commit -m "Description of changes"`
7. Push to your fork: `git push origin feature/your-feature-name`
8. Open a Pull Request

## Development Environment

### Prerequisites
- .NET SDK 8.0 or later
- Your favorite IDE (Visual Studio, VS Code, or Rider)

### Building
```bash
dotnet build
```

### Running Tests
```bash
dotnet test
```

### Running the CLI
```bash
cd src/Moxfield.Deck.Optimizer.CLI
dotnet run demo
```

## Code Style

- Follow standard C# naming conventions
- Use meaningful variable and method names
- Add XML documentation comments for public APIs
- Keep methods focused and single-purpose
- Write unit tests for new functionality

## Testing

- Write unit tests for all new features
- Ensure all tests pass before submitting a PR
- Aim for good test coverage of core logic
- Use descriptive test names that explain what is being tested

## Pull Request Guidelines

- Provide a clear description of the changes
- Reference any related issues
- Include tests for new functionality
- Ensure all tests pass
- Update documentation as needed
- Keep PRs focused on a single feature or fix

## Areas for Contribution

Some ideas for contributions:

### Features
- Add support for more collection sources (CSV import, etc.)
- Implement foil preference options
- Add card condition tracking
- Support for different card formats (Vintage, Modern, etc.)
- Integration with other deck building platforms

### Improvements
- Better error handling and user feedback
- Performance optimizations
- Enhanced Excel formatting options
- Additional export formats (CSV, PDF)
- Web-based interface

### Documentation
- More usage examples
- Video tutorials
- FAQ section
- Troubleshooting guide

### Testing
- Increase test coverage
- Add integration tests
- Performance benchmarks

## Questions?

Feel free to open an issue with your question or reach out to the maintainers.

Thank you for contributing!
