EDH Deck Builder Optimizer — Vision & Architecture

A tool and web app designed to help streamline EDH deck construction by analyzing your Moxfield collections and producing the most efficient physical card movement plan possible.

Overview

Building EDH decks from multiple binders and existing decks often means hunting through several collections, constantly swapping sleeves, and manually tracking which cards need to move where.
This project automates that process.

Given a set of collections from Moxfield, the system determines the optimal way to move cards from sources to targets and outputs a ready-to-use movement checklist in Excel format.

Core Concepts
Collections

A collection can be a binder, a deck, or any container of cards on Moxfield.
Each collection is defined by:

URL (Moxfield deck/collection link)

Name (extracted automatically from the URL)

Priority (1–5, default 3)

Active flag (inactive collections are ignored)

Role:

Source: cards can be taken from it

Target: cards should be placed into it

Collections act as the input for both the web app and the algorithm.

Algorithm Goals

The algorithm produces a card movement plan that respects several optimization rules.

1. Minimize the number of source collections

The best solution uses as few sources as possible.
Fewer binders and decks to open → faster physical sorting.

2. Maximize cards taken from any source that is used

Once a source is selected, the system tries to pull as many needed cards from it as possible.
This helps reduce sleeve changes and scattered card movement.

3. Respect collection priority

When two sources provide the same card, the one with the higher priority (lower number) is preferred.

4. Ignore inactive collections

Inactive sources and targets are excluded from calculations entirely.

Output: Excel Movement Plan

When the algorithm completes, the user downloads an Excel (.xlsx) file summarizing exactly which cards need to move and where.

Typical columns include:

Card Name

Source Collection

Target Collection

Set Code

Collector Number

Foil indicator

Quantity

This file acts as a checklist for reorganizing your physical cards.

Web Application

A lightweight web interface supports configuring collections, launching the algorithm, and retrieving results.

UI Flow

User enters Moxfield URLs for their collections.

The web app extracts and displays each collection’s name.

The user assigns:

priority

active/inactive

source/target role

The algorithm is executed on demand.

The Excel file is returned for download.

Backend Responsibilities

Fetch collection data from Moxfield only when needed.

Run the optimization algorithm.

Generate the Excel output.

Cache fetched collections to limit repeated Moxfield calls.

Architecture Goals

Minimal server footprint
Prefer a simple architecture: static frontend + small backend service.

Caching of collection data
Fetched Moxfield data should be retained until the user requests a refresh.

Deterministic results
Running the same input should always produce the same movement plan.

Future Roadmap

More detailed card metadata

Bulk URL import

Side-by-side deck comparison view

Undo/redo history of deck builds

Integration with additional card databases

Export to other formats (CSV, PDF)

Summary

This project provides a streamlined way to assemble EDH decks by optimizing card movement between your Moxfield collections. Through priority-based selection, source minimization, and intelligent grouping, it outputs a clean, actionable Excel file that saves time and reduces physical card handling.

It aims to be simple to host, easy to use, and accurate in guiding efficient deck building.
