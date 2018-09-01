import expect from 'expect';
import {authorsFormattedForDropdown} from './selectors';

describe('Author Selectors', () => {
  describe('authorsFormattedForDropdown', () => {
    it('should return author data formatted for use in a dropdown', () => {
      const authors = [
        {id: 'Iron Man', firstName: 'Stark', lastName: 'Anthony Edward'},
        {id: 'Spider-Man',firstName: 'Peter',lastName: 'Parker'}
      ];

      const expected = [
        {value: 'Iron Man', text: 'Stark Anthony Edward'},
        {value: 'Spider-Man', text: 'Peter Parker'}
      ];

      expect(authorsFormattedForDropdown(authors)).toEqual(expected);
    });
  });
});
