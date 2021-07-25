module.exports = `
    type Player {
        id: ID!
        name: String!
        playedGames: [PickupGame!]!
    }
`;